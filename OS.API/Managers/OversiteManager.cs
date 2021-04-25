using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.API.Managers.Interfaces;
using OS.API.Models.Oversite;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Managers
{
    public class OversiteManager : IOversiteManager
    {
        private readonly ILogger<OversiteManager> _Logger;
        private readonly IOversiteRepository _OversiteRepository;
        private readonly ISightRepository _SightRepository;

        public OversiteManager(ILogger<OversiteManager> logger, IOversiteRepository oversiteRepository, ISightRepository sightRepository)
        {
            _Logger = logger;
            _OversiteRepository = oversiteRepository;
            _SightRepository = sightRepository;
        }

        public async Task<List<OversiteModel>> GetAllAsync()
        {
            IQueryable<OversiteEntity> query = _OversiteRepository.AllOversitesQueryable();

            var dummySights = new[]
            {
                new SightModel { Data = "sight1 non buffer", OversiteId = 1 },
                new SightModel { Data = "sight2 non buffer", OversiteId = 1 }
            };

            return await query.Select(os => ConvertEntityToModel(os))
            .ToListAsync();

        }

        public async Task<OversiteModel> GetEntityAsync(int oversiteId)
        {
            var dbOversite = await _OversiteRepository.FindByIdAsync(oversiteId);

            return ConvertEntityToModel(dbOversite);
        }

        public async Task<List<OversiteModel>> GetBySearchResultAsync(string searchResult)
        {
            var dbOversites = await _OversiteRepository.FindBySearchResult(searchResult);

            if (dbOversites is null)
            {
                return null;
            }

            return dbOversites.ConvertAll<OversiteModel>(converter);
        }

        public async Task<OversiteModel> CreateAsync(OversiteFormData osFormData)
        {
            foreach (var key in osFormData.GetType().GetProperties())
            {
                if (key.Name.Equals("Sight")) continue;
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

            _Logger.LogInformation("Sights {1}", osSights.Count.ToString());

            foreach (var sight in osSights)
            {
                byte[] sightBytes;
                var stream = new MemoryStream();

                sight.CopyToAsync(stream);
                sightBytes = stream.ToArray();

                var fileBase64 = Convert.ToBase64String(sightBytes);

                var newSight = new SightEntity
                {
                    Data = fileBase64,
                    FileName = sight.FileName,
                    OversiteId = createdOversite.Id
                };

                var createdSights = await _SightRepository.AddSightAsync(newSight);
            }

            return ConvertEntityToModel(createdOversite);
        }

        private Converter<OversiteEntity, OversiteModel> converter = ConvertEntityToModel;
        private static OversiteModel ConvertEntityToModel(OversiteEntity os)
        {
            var dummySights = new[]
            {
                new SightModel { Data = "sight1 non buffer", OversiteId = 1 },
                new SightModel { Data = "sight2 non buffer", OversiteId = 1 }
            };

            return new OversiteModel
            {
                Title = os.Title,
                Domain = os.Domain,
                Severity = os.Severity,
                Description = os.Description,
                Category = os.Category,
                Private = os.Private,
                Sights = dummySights,
                UserId = os.UserId
            };
        }
    }
}
