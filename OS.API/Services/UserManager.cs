using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.API.Infrastructure.Interfaces;
using OS.API.Models.User;
using OS.API.Services.Interfaces;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services
{
    public class UserManager : IUserManager
    {
        private readonly ILogger<UserManager> _Logger;
        private readonly IUserRepository _UserRepository;
        private readonly IDateService _DateService;

        public UserManager(ILogger<UserManager> logger, IUserRepository userRepository, IDateService dateService)
        {
            _Logger = logger;
            _UserRepository = userRepository;
            _DateService = dateService;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            IQueryable<UserEntity> query = _UserRepository.GetQueryable();

            return await query.Select(user => new UserModel(user.Id)
            {
                Username = user.Username,
                JoinDate = user.JoinDate
            })
            .ToListAsync();
        }

        public AuthModel GetAuthDetails(string Username)
        {
            var userEntity = _UserRepository.FindByUsername(Username);

            if (userEntity is null)
            {
                return null;
            }

            return new AuthModel(userEntity.Id)
            {
                Username = userEntity.Username,
                Password = userEntity.Password
            };
        }

        public async Task<UserModel> GetModelAsync(int userId)
        {
            var userEntity = await _UserRepository.FindByIdAsync(userId);

            if (userEntity is null)
            {
                return null;
            }

            return new UserModel(userEntity.Id)
            {
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
                JoinDate = DateTime.Parse(_DateService.Now())
            };

            var createdEntity = await _UserRepository.AddAsync(userEntity);

            return new UserModel(createdEntity.Id)
            {
                Username = createdEntity.Username,
                JoinDate = createdEntity.JoinDate
            };
        }

        public async Task<UserModel> UpdateAsync(UpdateModel updateModel)
        {
            var userEntity = await _UserRepository.FindByIdAsync(updateModel.Id);

            if (userEntity is null)
            {
                return null;
            }

            userEntity.JoinDate = userEntity.JoinDate;
            userEntity.Username = UseUpdatedValue(userEntity.Username, updateModel.Username);
            userEntity.Password = UseUpdatedValue(userEntity.Password, updateModel.Password);

            var updatedEntity = await _UserRepository.UpdateAsync(userEntity);

            return new UserModel(updatedEntity.Id)
            {
                Username = updatedEntity.Username,
                JoinDate = updatedEntity.JoinDate
            };
        }

        public async Task DeleteAsync(int userId)
        {
            await _UserRepository.RemoveAsync(userId);
        }

        private string UseUpdatedValue(string currValue, string newValue)
        {
            var valueModified = !currValue.Equals(newValue);

            if (currValue.Equals("")  || currValue is null) return newValue;
            if (newValue.Equals("") || newValue is null) return currValue;

            if (valueModified)
            {
                return currValue;
            } else
            {
                return newValue;
            }
        }
    }
}
