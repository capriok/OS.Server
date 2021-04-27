using OS.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Managers.Interfaces
{
    public interface IUserManager
    {
        Task<AuthModel> GetAuthDetails(string Username);
        Task<UserModel> GetModelAsync(int id);
        Task<UserModel> CreateAsync(AuthModel user);
        Task<UserModel> UpdateAsync(UpdateModel user);
        Task DeleteAsync(int userId);
    }
}
