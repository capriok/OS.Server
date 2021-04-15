using OS.API.Models.User;
using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface IUserManager
    {
        Task<List<UserModel>> GetAllAsync();
        AuthModel GetAuthDetails(string Username);
        Task<UserModel> GetModelAsync(int id);
        Task<UserModel> CreateAsync(AuthModel user);
        Task<UserModel> UpdateAsync(UpdateModel user);
        Task DeleteAsync(int userId);
    }
}
