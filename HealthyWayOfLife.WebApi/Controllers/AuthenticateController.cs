using System;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Model.ControllerParam;
using HealthyWayOfLife.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthyWayOfLife.WebApi.Controllers
{
    public class AuthenticateController : BaseController
    {
        private readonly AuthenticationHandlerService _authenticationHandlerService;

        public AuthenticateController(AuthenticationHandlerService authenticationHandlerService)
        {
            _authenticationHandlerService = authenticationHandlerService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<bool>> Login(LoginParameters loginParameters)
        {
            return await _authenticationHandlerService.Login(loginParameters);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Logout()
        {
            throw new NotImplementedException();
            //return await _mediator.Send(new LogoutCommand());
        }

        [HttpPost]
        public async Task<ActionResult<bool>> ResetPassword(/*ResetPasswordCommand resetPasswordCommand*/)
        {
            throw new NotImplementedException();
            //if (!await _mediator.Send(new CheckIfPasswordResetTokenIsActiveCommand()
            //{ Guid = resetPasswordCommand.Guid }))
            //{
            //    throw new CustomCodeException(System.Net.HttpStatusCode.Unauthorized, "Error", LogType.Warning);
            //}
            //return await _mediator.Send(resetPasswordCommand);
        }

        [HttpGet]
        public async Task<ActionResult<bool>> CheckIfPasswordResetTokenIsActive(/*Guid guid*/)
        {
            throw new NotImplementedException();
            //return await _mediator.Send(new CheckIfPasswordResetTokenIsActiveCommand() { Guid = guid });
        }
    }
}