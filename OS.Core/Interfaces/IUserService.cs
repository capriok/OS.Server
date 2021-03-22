using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Interfaces
{
    public interface IUserService
    {
        Task<List<Core.Models.User>> GetUsersAsync();
        Entities.User GetUserByUsernameAsync(string Username);
        Task<Entities.User> GetUserEntityAsync(int Id);
        Task<Core.Models.User> GetUserModelAsync(int Id);
        Task<Core.Models.User> CreateUserAsync(Entities.User user);
        Task<Core.Models.User> UpdateUserAsync(Entities.User user);
        Task DeleteUserAsync(int Id);
    }
}
