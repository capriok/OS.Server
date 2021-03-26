using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.API.Contracts.Models.User;
using OS.API.Services.Interfaces;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _log;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _log = logger;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            IQueryable<UserEntity> query = _userRepository.GetQueryable();

            return await query.Select(user => new UserModel
            {
                Id = user.Id,
                Username = user.Username,
                JoinDate = user.JoinDate
            })
            .ToListAsync();
        }
        public UserEntity GetOneAuthDetails(string Username)
        {
            var userEntity = _userRepository.FindByUsername(Username);

            if (userEntity is null)
            {
                return null;
            }

            return userEntity;
        }

        public async Task<UserEntity> GetOneEntityAsync(int Id)
        {
            var userEntity = await _userRepository.FindByIdAsync(Id);

            if (userEntity is null)
            {
                return null;
            }

            return userEntity;
        }

        public async Task<UserModel> GetOneModelAsync(int Id)
        {
            var userEntity = await _userRepository.FindByIdAsync(Id);

            if (userEntity is null)
            {
                return null;
            }

            return new UserModel
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                JoinDate = userEntity.JoinDate
            };
        }

        public async Task<UserModel> CreateAsync(AuthModel authModel)
        {

            var userEntity = new UserEntity
            {
                Username = authModel.Username,
                Password = authModel.Password,
                JoinDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("T", " "))
        };

            var createdEntity = await _userRepository.AddAsync(userEntity);

            _log.LogInformation(createdEntity.JoinDate.ToString());

            return new UserModel
            {
                Id = createdEntity.Id,
                Username = createdEntity.Username,
                JoinDate = createdEntity.JoinDate
            };
        }

        public async Task<UserModel> UpdateAsync(UpdateModel reqModel)
        {
            var userEntity = new UserEntity
            {
                Id = reqModel.Id,
                Username = reqModel.Username,
                Password = reqModel.Password
            };

            var updatedEntity = await _userRepository.UpdateAsync(userEntity);

            return new UserModel
            {
                Id = updatedEntity.Id,
                Username = updatedEntity.Username,
                JoinDate = updatedEntity.JoinDate
            };
        }

        public async Task DeleteUserAsync(int Id)
        {
            await _userRepository.RemoveAsync(Id);
        }
    }
}
