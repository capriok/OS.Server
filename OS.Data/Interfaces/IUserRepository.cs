using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OS.Data.Entities;

namespace OS.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Find(int id);
        Task<User> Update(User user);
        Task<User> Add(User user);
        Task Remove(int id);
        IQueryable<User> Get();
    }
}
