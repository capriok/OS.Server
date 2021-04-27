using Microsoft.Extensions.Logging;
using OS.API.Managers.Interfaces;
using OS.API.Models.Oversite;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OS.API.Managers
{
    public class OversiteManager : IOversiteManager
    {
        private readonly ILogger<OversiteManager> _Logger;
        private readonly IOversiteRepository _OversiteRepository;
        private readonly ISightManager _SightManager;
        private readonly IUserManager _UserManager;

        public OversiteManager(ILogger<OversiteManager> logger, IOversiteRepository oversiteRepository, ISightManager sightManager, IUserManager userManager)
        {
            _Logger = logger;
            _OversiteRepository = oversiteRepository;
            _SightManager = sightManager;
            _UserManager = userManager;
        }

        public async Task<List<OversiteModel>> GetRecentAsync()
        {
            var dbOversites = await _OversiteRepository.FindTenRecent();

            return ConvertAllToModels(dbOversites);
        }

        public async Task<OversiteModel> GetModelAsync(int oversiteId)
        {
            var dbOversite = await _OversiteRepository.FindByIdAsync(oversiteId);
            var dbSights = await _SightManager.FindByOversiteId(oversiteId);
            var dbOsFounder = await _UserManager.GetModelAsync(dbOversite.UserId);

            var osModel = ConvertEntityToModel(dbOversite);

            osModel.Sights = dbSights;
            osModel.Founder = dbOsFounder.Username;

            return osModel;
        }

        public async Task<List<OversiteModel>> GetBySearchResultAsync(string searchResult)
        {
            var dbOversites = await _OversiteRepository.FindBySearchResult(searchResult);

            if (dbOversites is null)
            {
                return null;
            }

            return ConvertAllToModels(dbOversites);
        }

        public async Task<OversiteModel> CreateAsync(OversiteFormData osFormData)
        {
            foreach (var key in osFormData.GetType().GetProperties())
            {
                _Logger.LogInformation("Key: {1} Value: {2}", key.Name, key.GetValue(osFormData));
            }

            var newOversite = new OversiteEntity
            {
                Title = osFormData.Title,
                Domain = osFormData.Domain,
                Severity = osFormData.Severity,
                Description = osFormData.Description,
                Category = osFormData.Category,
                Private = Convert.ToBoolean(osFormData.Private),
                UserId = Convert.ToInt32(osFormData.UserId)
            };

            var createdOversite = await _OversiteRepository.AddOversiteAsync(newOversite);

            var osSights = osFormData.Sights;

            if (osSights is not null)
            {
                _Logger.LogInformation("Sights To Create {1}", osSights.Count.ToString());

                await _SightManager.CreateRangeAsync(createdOversite.Id, osSights);
            }

            return ConvertEntityToModel(createdOversite);
        }

        private Converter<OversiteEntity, OversiteModel> converter = ConvertEntityToModel;
        private List<OversiteModel> ConvertAllToModels(List<OversiteEntity> osList)
        {
            return osList.ConvertAll(converter);
        }

        private static OversiteModel ConvertEntityToModel(OversiteEntity os)
        {
            if (os is null)
            {
                return null;
            }

            return new OversiteModel
            {
                Id = os.Id,
                Title = os.Title,
                Domain = os.Domain,
                Severity = os.Severity,
                Description = os.Description,
                Category = os.Category,
                Private = os.Private,
                UserId = os.UserId
            };
        }
    }
}
