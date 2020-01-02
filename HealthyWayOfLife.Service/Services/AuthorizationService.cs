using System;
using System.Net;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Model.Model.Database;

namespace HealthyWayOfLife.Service.Services
{
    // todo remove old
    public class AuthorizationService
    {
        private readonly CryptoService _cryptoService;
        private readonly IUserSessionRepository _userSessionRepository;

        public AuthorizationService(CryptoService cryptoService, IUserSessionRepository userSessionRepository)
        {
            _cryptoService = cryptoService;
            _userSessionRepository = userSessionRepository;
        }

        public virtual bool CheckCredentials(User user, string insertedPassword)
        {
            if (_cryptoService.CreateMd5(insertedPassword) != user.Password)
            {
                return false;
            }

            return true;
        }

        public virtual string GenerateToken(User user)
        {
            return "";
        }

        public virtual async Task InsertSessionToDb(Session userSession)
        {
            try
            {
                await _userSessionRepository.InsertUserSession(userSession).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw new CustomCodeException(HttpStatusCode.InternalServerError, "Błąd dodawania sesji do bazy.", LogType.Error);
            }

        }

        public virtual Session CreateSession(User user, string token)
        {
            return new Session()
            {
                User = user
            };
        }

        public virtual Task<Session> GetExistingUserSession(string token = null)
        {
            return _userSessionRepository.GetExistingUserSessionWithUser(token);
        }

        public virtual async Task RefreshSession(Session session, int sessionTime)
        {
            if (session is null)
            {
                throw new CustomCodeException(HttpStatusCode.Forbidden, "Brak tokenu.", LogType.Warning);
            }

            if (session.ExpirationDate.Subtract(DateTime.UtcNow).TotalMinutes < 0)
            {
                session.SessionState = SessionState.Closed;
                session.EndTime = DateTime.UtcNow;
                await _userSessionRepository.UpdateSessionRefreshInfo(session);
                throw new CustomCodeException(HttpStatusCode.Forbidden, "", LogType.Warning);
            }

            if (session.SessionState != SessionState.Open)
            {
                throw new CustomCodeException(HttpStatusCode.Forbidden, "", LogType.Warning);
            }

            session.LastRefreshDate = DateTime.UtcNow;
            session.ExpirationDate = DateTime.UtcNow.AddMinutes(sessionTime);
            await _userSessionRepository.UpdateSessionRefreshInfo(session);
        }

        public virtual async Task EndSession(string token = null)
        {
            Session session = await _userSessionRepository.GetExistingUserSession(token);
            if (session == null || session.EndTime != null || session.SessionState != SessionState.Open)
            {
                return;
            }

            session.EndTime = DateTime.UtcNow;
            session.SessionState = SessionState.Closed;

            await _userSessionRepository.UpdateSessionRefreshInfo(session);
        }

    }
}