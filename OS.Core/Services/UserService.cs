using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OS.Data.Interfaces;

namespace OS.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<Models.User>> GetUsersAsync()
        {
            IQueryable<Data.Entities.User> query = _userRepository.GetQueryable();

            return await query.Select(user => new Models.User
            {
                Id = user.Id,
                Username = user.Username,
                JoinDate = user.JoinDate
            })
            .ToListAsync();
        }
        public Data.Entities.User GetUserByUsernameAsync(string Username)
        {
            var userEntity = _userRepository.FindByUsernameAsync(Username);

            if (userEntity is null)
            {
                return null;
            }

            return userEntity;
        }

        public async Task<Data.Entities.User> GetUserEntityAsync(int Id)
        {
            var userEntity = await _userRepository.FindByIdAsync(Id);

            if (userEntity is null)
            {
                return null;
            }

            return userEntity;

        }

        public async Task<Models.User> GetUserModelAsync(int Id)
        {
            var userEntity = await _userRepository.FindByIdAsync(Id);

            if (userEntity is null)
            {
                return null;
            }

            return new Models.User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                JoinDate = userEntity.JoinDate
            };
        }

        public async Task<Models.User> CreateUserAsync(Data.Entities.User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            string mySqlCurrentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            var userEntity = new Data.Entities.User
            {
                Username = user.Username,
                Password = user.Password,
                JoinDate = DateTime.Parse(mySqlCurrentDateTime)
            };

            userEntity = await _userRepository.AddAsync(userEntity);

            return new Models.User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                JoinDate = userEntity.JoinDate
            };
        }

        public async Task<Models.User> UpdateUserAsync(Data.Entities.User user)
        {
            var userEntity = await _userRepository.UpdateAsync(user);

            return new Models.User
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                JoinDate = userEntity.JoinDate
            };
        }

        public async Task DeleteUserAsync(int Id)
        {
            await _userRepository.RemoveAsync(Id);
        }
    }
}
