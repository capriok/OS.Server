using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OS.Core.Models;

namespace OS.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();
        Task<User> GetUser(int Id);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int Id);
    }
}
