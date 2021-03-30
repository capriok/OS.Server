using OS.API.Contracts.Models.User;
using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllAsync();
        AuthModel GetOneAuthDetails(string Username);
        Task<UserEntity> GetOneEntityAsync(int Id);
        Task<UserModel> GetOneModelAsync(int Id);
        Task<UserModel> CreateAsync(AuthModel user);
        Task<UserModel> UpdateAsync(UpdateModel user);
        Task DeleteUserAsync(int Id);
    }
}
