using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace HealthyWayOfLife.WebApi.Options
{
    public class WebApiExceptionHandlerOptions : IConfigureOptions<ExceptionHandlerOptions>
    {
        private readonly IExceptionService _exceptionService;

        public WebApiExceptionHandlerOptions(IExceptionService exceptionService)
        {
            _exceptionService = exceptionService;
        }

        public void Configure(ExceptionHandlerOptions exceptionHandlerOptions) =>
            exceptionHandlerOptions.ExceptionHandler = httpContext =>
            {
                _exceptionService.HandleException(httpContext.Features.Get<IExceptionHandlerPathFeature>().Error);
                return Task.CompletedTask;
            };
    }
}