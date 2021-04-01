using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OS.API.Infrastructure.Interfaces;
using OS.API.Models.User;
using OS.API.Services.Interfaces;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OS.API.Services
{
    public class TestModel
    {
        public int Idx { get; set; }
    }
    public class UserManager : IUserManager
    {
        private readonly ILogger<UserManager> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IDateService _dateService;

        public UserManager(ILogger<UserManager> logger, IUserRepository userRepository, IDateService dateService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _dateService = dateService;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            IQueryable<UserEntity> query = _userRepository.GetQueryable();

            return await query.Select(user => new UserModel(user.Id)
            {
                Username = user.Username,
                JoinDate = user.JoinDate
            })
            .ToListAsync();
        }

        public AuthModel GetOneAuthDetails(string Username)
        {
            var userEntity = _userRepository.FindByUsername(Username);

            if (userEntity is null)
            {
                return null;
            }

            return new AuthModel(userEntity.Id)
            {
                Username = userEntity.Username,
                Password = userEntity.Password,
                RefreshToken = userEntity.RefreshToken
            };
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
                JoinDate = DateTime.Parse(_dateService.Now())
            };

            var createdEntity = await _userRepository.AddAsync(userEntity);

            return new UserModel(createdEntity.Id)
            {
                Username = createdEntity.Username,
                JoinDate = createdEntity.JoinDate
            };
        }

        public async Task<UserModel> UpdateAsync(UpdateModel updateModel)
        {
            var userEntity = await _userRepository.FindByIdAsync(updateModel.Id);

            userEntity.Username = userEntity.Username;
            userEntity.JoinDate = userEntity.JoinDate;
            userEntity.Password = userEntity.Password;
            userEntity.RefreshToken = UseUpdatedValue(userEntity.RefreshToken, updateModel.RefreshToken);

            var updatedEntity = await _userRepository.UpdateAsync(userEntity);

            return new UserModel(updatedEntity.Id)
            {
                Username = updatedEntity.Username,
                JoinDate = updatedEntity.JoinDate
            };
        }

        public async Task DeleteUserAsync(int Id)
        {
            await _userRepository.RemoveAsync(Id);
        }

        private string UseUpdatedValue(string currValue, string newValue)
        {
            if (currValue is null)
            {
                return newValue;
            }
            if (!newValue.Equals(""))
            {
                return currValue;
            }

            var valueModified = !currValue.Equals(newValue);

            if (valueModified)
            {
                return newValue;
            }
            else
            {
                return currValue;
            }
        }
    }
}