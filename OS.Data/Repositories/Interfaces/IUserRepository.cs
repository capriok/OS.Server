using OS.Data;
using OS.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> FindByIdAsync(int id);
        UserEntity FindByUsername(string Username);
        Task<UserEntity> UpdateAsync(UserEntity user);
        Task<UserEntity> AddAsync(UserEntity user);
        Task RemoveAsync(int id);
        IQueryable<UserEntity> GetQueryable();
    }
}
