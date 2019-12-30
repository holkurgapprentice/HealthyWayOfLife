using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Interfaces;
using HealthyWayOfLife.Model.Model.ControllerParam;
using HealthyWayOfLife.Model.Model.Database;
using HealthyWayOfLife.Model.Model.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace HealthyWayOfLife.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HealthyWayOfLifeDbContext _context;
        private readonly GlobalConfig _globalConfig;

        public UserRepository(HealthyWayOfLifeDbContext context, GlobalConfig globalConfig)
        {
            _context = context;
            _globalConfig = globalConfig;
        }

        public Task<User> GetUser(string email)
        {
            return _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public Task<User> GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetUserByMail(string mail)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<UserListItem>> GetUserList(GetUsersParams usersViewModelCommand)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertUserWithSave(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckIfLoginIsAvailable(string login)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckIfMailIsAvailable(int userId, string mail)
        {
            throw new System.NotImplementedException();
        }
    }
}