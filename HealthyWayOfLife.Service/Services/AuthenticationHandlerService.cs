using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Enums;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model;
using HealthyWayOfLife.Model.Model.ControllerParam;
using HealthyWayOfLife.Model.Model.Database;
using HealthyWayOfLife.Model.Model.ViewModel;
using HealthyWayOfLife.Repository;
using HealthyWayOfLife.Repository.Repositories;

namespace HealthyWayOfLife.Service.Services
{
    public class AuthenticationHandlerService
    {
        private readonly GlobalConfig _globalConfig;
        private readonly EnumService _enumService;
        private readonly AuthorizationService _authorizationService;
        private readonly IUserRepository _usersRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly TransactionService<HealthyWayOfLifeDbContext> _transactionService;

        public AuthenticationHandlerService(GlobalConfig globalConfig, EnumService enumService, AuthorizationService authorizationService, IUserRepository usersRepository,
            IUserSessionRepository userSessionRepository, TransactionService<HealthyWayOfLifeDbContext> transactionService)
        {
            _globalConfig = globalConfig;
            _enumService = enumService;
            _authorizationService = authorizationService;
            _usersRepository = usersRepository;
            _userSessionRepository = userSessionRepository;
            _transactionService = transactionService;
        }
      
        public async Task<bool> Login(LoginParameters request)
        {
            User user = await _usersRepository.GetUser(request.Email).ConfigureAwait(false);

            if (user == null || !_authorizationService.CheckCredentials(user, request.Password))
            {
                throw new CustomCodeException(HttpStatusCode.Unauthorized, "");
            }

            if (!user.IsActive)
            {
                throw new CustomCodeException(HttpStatusCode.Unauthorized, "");
            }

            using (Transaction transaction = _transactionService.BeginTransaction())
            {
                await _userSessionRepository.CloseOpenSessions(user.Id);

                string token = _authorizationService.GenerateToken(user);
                await _authorizationService.InsertSessionToDb(_authorizationService.CreateSession(user, token));

                await transaction.Commit();
            }
            
            return true;
        }
    }
}