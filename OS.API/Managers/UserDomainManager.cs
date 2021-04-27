using OS.API.Managers.Interfaces;
using OS.API.Models.User;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OS.API.Managers
{
    public class UserDomainManager : IUserDomainManager
    {
        private readonly IUserDomainRepository _UserDomainRepository;

        public UserDomainManager(IUserDomainRepository userDomainRepository)
        {
            _UserDomainRepository = userDomainRepository;
        }

        public async Task<List<UserDomainModel>> GetAllByUserId(int userId)
        {
            var dbUserdomains = await _UserDomainRepository.FindByUserIdQuery(userId);

            var userDomains = new List<UserDomainModel>();

            if (dbUserdomains.Count <= 0)
            {
                return userDomains;
            }

            return ConvertAllToModels(dbUserdomains);
        }

        private Converter<UserDomainEntity, UserDomainModel> converter = ConvertEntityToModel;
        private List<UserDomainModel> ConvertAllToModels(List<UserDomainEntity> sightList)
        {
            return sightList.ConvertAll(converter);
        }
        private static UserDomainModel ConvertEntityToModel(UserDomainEntity d)
        {
            if (d is null)
            {
                return null;
            }

            return new UserDomainModel(d.Id)
            {
                Domain = d.Domain,
                UserId = d.UserId
            };
        }
    }
}
