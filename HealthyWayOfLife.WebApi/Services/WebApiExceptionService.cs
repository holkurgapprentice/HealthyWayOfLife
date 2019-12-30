using System;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Service.Services;
using Microsoft.AspNetCore.Http;

namespace HealthyWayOfLife.WebApi.Services
{
    public class WebApiExceptionService : ExceptionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebApiExceptionService(IHttpContextAccessor httpContextAccessor, ILogRepository<HealthyWayOfLifeDbContext> logRepository)
            : base(logRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            //todo 
            //separate token service (read token details, provide claims, check if sign is ok, separate from WebApiAuthorizationService)
            //read claims for user and session or just JWT,
            //read context for Request path & body
            //both store in .log table
        }

        public override async void HandleException(Exception exception)
        {
            base.HandleException(exception);

            if (exception is CustomCodeException customCodeException)
            {
                // httpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                _httpContextAccessor.HttpContext.Response.StatusCode = (int)customCodeException.StatusCode;
                await _httpContextAccessor.HttpContext.Response.WriteAsync(customCodeException.ExceptionString);
            }
        }
    }
}