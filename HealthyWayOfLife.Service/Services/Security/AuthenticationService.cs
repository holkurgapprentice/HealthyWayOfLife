using System;
using System.Net;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Interfaces.Security;
using HealthyWayOfLife.Model.Models;
using HealthyWayOfLife.Model.Models.Database;
using HealthyWayOfLife.Model.Models.Dtos;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;
using Microsoft.AspNetCore.Http;

namespace HealthyWayOfLife.Service.Services.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        // check if user is that user, provide user permission schema and user details

        private readonly IUserRepository _usersRepository;
        private readonly ITokenService _tokenService;
        private readonly CryptoService _cryptoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly TransactionService<HealthyWayOfLifeDbContext> _transactionService;
        private readonly GlobalConfig _globalConfig;
        private int SessionTimeMinutes => _globalConfig.SessionTimeMinutes;
        
        public AuthenticationService(IUserRepository usersRepository, ITokenService tokenService, CryptoService cryptoService, IHttpContextAccessor httpContextAccessor,
            IUserSessionRepository userSessionRepository, TransactionService<HealthyWayOfLifeDbContext> transactionService, GlobalConfig globalConfig)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
            _cryptoService = cryptoService;
            _httpContextAccessor = httpContextAccessor;
            _userSessionRepository = userSessionRepository;
            _transactionService = transactionService;
            _globalConfig = globalConfig;
        }

        public async Task<LoginRequestDto> Login(LoginRequestDto request)
        {
            User user = await _usersRepository.GetUser(request.Email).ConfigureAwait(false);

            if (user == null || !CheckCredentials(user, request.Password))
                throw new HwolException(HttpStatusCode.Unauthorized, "");

            if (!user.IsActive)
                throw new HwolException(HttpStatusCode.Unauthorized, "");

            using (Transaction transaction = _transactionService.BeginTransaction())
            {
                await _userSessionRepository.CloseOpenSessions(user.Id);

                request.Token = _tokenService.GenerateTokenStringForUser(user);
                await InsertSessionToDb(CreateSession(user, request.Token));
                await transaction.Commit();
            }

            request.Password = "";

            return request;
        }

        
        public async Task InsertSessionToDb(Session userSession)
        {
            try
            {
                await _userSessionRepository.InsertUserSession(userSession).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new HwolException(HttpStatusCode.InternalServerError, "Error adding session to Db.", LogType.Error);
            }

        }

        public Session CreateSession(User user, string token)
        {
            var result = new Session()
            {
                User = user
            };

            result.RemoteAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            result.SessionState = SessionState.Open;
            result.StartTime = DateTime.UtcNow;
            result.ExpirationDate = DateTime.UtcNow.AddMinutes(SessionTimeMinutes);
            result.Token = token;

            return result;
        }

        public async Task RefreshSession(Session session, int sessionTime)
        {
            if (session is null)
            {
                throw new HwolException(HttpStatusCode.Forbidden, "Brak tokenu.", LogType.Warning);
            }

            if (session.ExpirationDate.Subtract(DateTime.UtcNow).TotalMinutes < 0)
            {
                session.SessionState = SessionState.Closed;
                session.EndTime = DateTime.UtcNow;
                await _userSessionRepository.UpdateSessionRefreshInfo(session);
                throw new HwolException(HttpStatusCode.Forbidden, "", LogType.Warning);
            }

            if (session.SessionState != SessionState.Open)
            {
                throw new HwolException(HttpStatusCode.Forbidden, "", LogType.Warning);
            }

            session.LastRefreshDate = DateTime.UtcNow;
            session.ExpirationDate = DateTime.UtcNow.AddMinutes(sessionTime);
            await _userSessionRepository.UpdateSessionRefreshInfo(session);
            //todo zwroc nowy token
        }

        public async Task EndSession(string token)
        {
            if (token == null)
                return;

            Session session = await _userSessionRepository.GetExistingUserSession(token);
            if (session == null || session.EndTime != null || session.SessionState != SessionState.Open)
            {
                return;
            }

            session.EndTime = DateTime.UtcNow;
            session.SessionState = SessionState.Closed;

            await _userSessionRepository.UpdateSessionRefreshInfo(session);
        }

        public Task<Session> GetExistingUserSession(string token = null)
        {
            token = _httpContextAccessor.HttpContext.Request.Headers["Authorize"];
            return _userSessionRepository.GetExistingUserSessionWithUser(token);
        }

        private bool CheckCredentials(User user, string insertedPassword)
        {
            if (_cryptoService.CreateMd5(insertedPassword) != user.Password)
                return false;
            else
                return true;
        }

    }
}