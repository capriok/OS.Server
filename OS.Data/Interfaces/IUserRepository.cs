using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OS.Data;

namespace OS.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<Entities.User> FindByIdAsync(int id);
        Entities.User FindByUsername(string Username);
        Task<Entities.User> UpdateAsync(Entities.User user);
        Task<Entities.User> AddAsync(Entities.User user);
        Task RemoveAsync(int id);
        IQueryable<Entities.User> GetQueryable();
    }
}
