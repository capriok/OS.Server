using Microsoft.EntityFrameworkCore;
using OS.API.Models.User;
using OS.API.Services.Interfaces;
using OS.Data.Entities.User;
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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                Password = authModel.Password
            };

            var createdEntity = await _userRepository.AddAsync(userEntity);

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
