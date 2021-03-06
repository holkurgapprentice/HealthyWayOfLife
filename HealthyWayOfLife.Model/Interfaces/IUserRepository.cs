﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HealthyWayOfLife.Model.Models.Database;
using HealthyWayOfLife.Model.Models.Dtos;

namespace HealthyWayOfLife.Model.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUser(string email);
        Task<User> GetUser(int id);
        Task<User> GetUserByMail(string mail);
        Task<List<UserListItem>> GetUserList(GetUsersParams usersViewModelCommand);
        Task InsertUserWithSave(User user);
        Task<bool> CheckIfLoginIsAvailable(string login);
        Task<bool> CheckIfMailIsAvailable(int userId, string mail);
    }
}