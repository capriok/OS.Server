using Microsoft.Extensions.Logging;
using OS.API.Infrastructure.Interfaces;
using OS.API.Managers.Interfaces;
using OS.API.Models.User;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace OS.API.Managers
{
    public class UserManager : IUserManager
    {
        private readonly ILogger<UserManager> _Logger;
        private readonly IUserRepository _UserRepository;
        private readonly IUserDomainManager _UserDomainManager;
        private readonly IDateService _DateService;

        public UserManager(ILogger<UserManager> logger, IUserRepository userRepository, IUserDomainManager userDomainManager, IDateService dateService)
        {
            _Logger = logger;
            _UserRepository = userRepository;
            _UserDomainManager = userDomainManager;
            _DateService = dateService;
        }

        public async Task<AuthModel> GetAuthDetails(string Username)
        {
            var userEntity = await _UserRepository.FindByUsername(Username);

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

            var userDomains = await _UserDomainManager.GetAllByUserId(userId);

            return new UserModel(userEntity.Id)
            {
                Username = userEntity.Username,
                JoinDate = userEntity.JoinDate,
                Domains = userDomains
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

            return ConvertEntityToModel(createdEntity);
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

            return ConvertEntityToModel(updatedEntity);
        }

        public async Task DeleteAsync(int userId)
        {
            await _UserRepository.RemoveAsync(userId);
        }

        private string UseUpdatedValue(string currValue, string newValue)
        {
            var valueModified = !currValue.Equals(newValue);

            if (currValue.Equals("") || currValue is null) return newValue;
            if (newValue.Equals("") || newValue is null) return currValue;

            if (valueModified)
            {
                return currValue;
            }
            else
            {
                return newValue;
            }
        }

        private Converter<UserEntity, UserModel> converter = ConvertEntityToModel;
        private static UserModel ConvertEntityToModel(UserEntity u)
        {
            if (u is null)
            {
                return null;
            }

            return new UserModel (u.Id) 
            {
                Username = u.Username,
                JoinDate = u.JoinDate
            };
        }
    }
}
