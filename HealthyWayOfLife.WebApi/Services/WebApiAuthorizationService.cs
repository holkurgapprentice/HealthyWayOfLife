using System;
using System.Net;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Model.Model.Database;
using HealthyWayOfLife.Repository.Repositories;
using HealthyWayOfLife.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HealthyWayOfLife.WebApi.Services
{
    // todo remove old
    public class WebApiAuthorizationService : AuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly GlobalConfig _globalConfig;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        private int SessionTimeMinutes =>
            _globalConfig.SessionTimeMinutes;


        public WebApiAuthorizationService(IConfiguration configuration, CryptoService cryptoService, IHttpContextAccessor httpContextAccessor, IUserSessionRepository userSessionRepository, GlobalConfig globalConfig, ITokenService tokenService)
            : base(cryptoService, userSessionRepository)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _globalConfig = globalConfig;
            _tokenService = tokenService;
        }


        public override Task<Session> GetExistingUserSession(string token = null)
        {
            token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            return base.GetExistingUserSession(token);
        }

        public override string GenerateToken(User user)
        {
            try
            {
                string token = _tokenService.GenerateTokenStringForUser(user);
                //_httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", token, new CookieOptions()
                //{
                //    Expires = DateTime.UtcNow.AddMinutes(SessionTimeMinutes),
                //    Secure = true,
                //    Domain = _configuration.GetSection("WebsiteSettings")["Domain"],
                //    HttpOnly = true,
                //    SameSite = SameSiteMode.None
                //});
                _httpContextAccessor.HttpContext.Response.Headers.Add("token", token);

                //todo remove
                _tokenService.GetClaimsFromToken(token);

                return token;
            }
            catch (Exception exception)
            {
                throw new CustomCodeException(HttpStatusCode.Conflict, exception.Message, LogType.Error);
            }
        }

        public override Session CreateSession(User user, string token)
        {
            Session session = base.CreateSession(user, token);
            session.RemoteAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            session.SessionState = SessionState.Open;
            session.StartTime = DateTime.UtcNow;
            session.ExpirationDate = DateTime.UtcNow.AddMinutes(SessionTimeMinutes);
            session.Token = token;

            return session;
        }

        public override async Task RefreshSession(Session session, int sessionTime)
        {
            await base.RefreshSession(session, sessionTime).ConfigureAwait(false);
            //_httpContextAccessor.HttpContext.Response.Cookies.Append("jwt", session.Token, new CookieOptions()
            //{
            //    Expires = DateTime.UtcNow.AddMinutes(SessionTimeMinutes),
            //    Secure = true,
            //    Domain = _configuration.GetSection("WebsiteSettings")["Domain"],
            //    HttpOnly = true,
            //    SameSite = SameSiteMode.None

            //});
            _httpContextAccessor.HttpContext.Response.Headers.Add("token", session.Token);
        }

        public override async Task EndSession(string token = null)
        {
            token = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            await base.EndSession(token);
        }
    }
}