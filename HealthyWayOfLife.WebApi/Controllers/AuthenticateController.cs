using System;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Interfaces.Security;
using HealthyWayOfLife.Model.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthyWayOfLife.WebApi.Controllers
{
    public class AuthenticateController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticateController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<LoginRequestDto>> Login(LoginRequestDto loginRequestDto)
        {
            return await _authenticationService.Login(loginRequestDto);
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
            //    throw new HwolException(System.Net.HttpStatusCode.Unauthorized, "Error", LogType.Warning);
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