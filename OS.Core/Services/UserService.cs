using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OS.Core.Models;
using OS.Data.Interfaces;

namespace OS.Core.Services
{
    class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<List<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
        public Task<User> GetUser(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
