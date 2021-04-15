using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<UserEntity> GetQueryable();
        UserEntity FindByUsername(string Username);
        Task<UserEntity> FindByIdAsync(int id);
        Task<UserEntity> AddAsync(UserEntity user);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task RemoveAsync(int userId);
    }
}
