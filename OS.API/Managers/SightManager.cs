using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OS.API.Managers.Interfaces;
using OS.API.Models.Oversite;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OS.API.Managers
{
    public class SightManager : ISightManager
    {
        private readonly ILogger<SightManager> _Logger;
        private readonly ISightRepository _SightRepository;

        public SightManager(ILogger<SightManager> logger, ISightRepository sightRepository)
        {
            _Logger = logger;
            _SightRepository = sightRepository;
        }

        public async Task<List<SightModel>> FindByOversiteId(int oversiteId)
        {
            var dbSights = await _SightRepository.GetOversitesSights(oversiteId);

            foreach (var sight in dbSights)
            {
                _Logger.LogInformation("(test) Has Sight Bytes: {1}", sight.Data.Length > 0);
            }

            var osSights = new List<SightModel>();

            if (dbSights.Count <= 0)
            {
                return osSights;
            }

            return ConvertAllToModels(dbSights);
        }
        public async Task<SightModel> CreateAsync(int oversiteId, IFormFile sight)
        {
            var sightBytes = await ConvertSightToBytes(sight);

            var newSight = new SightEntity
            {
                Data = sightBytes,
                FileName = sight.FileName,
                OversiteId = oversiteId
            };

            var createdSight = await _SightRepository.AddSightAsync(newSight);

            return ConvertEntityToModel(createdSight);
        }

        public async Task<List<SightModel>> CreateRangeAsync(int oversiteId, IFormFileCollection osSights)
        {
            const int MAX_UPLOAD_MB = 4000000;

            var createdSights = new List<SightEntity>() { };
            int totalAdded = 0;

            while (totalAdded < osSights.Count)
            {
                var sightList = new List<SightEntity>() { };
                int totalRange = 0;

                for (int i = totalAdded; i < osSights.Count; i++)
                {
                    var sight = osSights[i];
                    var sightBytes = await ConvertSightToBytes(sight);

                    if (totalRange + sightBytes.Length >= MAX_UPLOAD_MB)
                    {
                        i = osSights.Count;
                        continue;
                    }

                    totalRange = totalRange += sightBytes.Length;

                    sightList.Add(new SightEntity
                    {
                        Data = sightBytes,
                        FileName = sight.FileName,
                        OversiteId = oversiteId
                    });
                }

                var created = await _SightRepository.AddSightRangeAsync(sightList);
                totalAdded = totalAdded + created.Count;
                createdSights.AddRange(created);
            }

            return ConvertAllToModels(createdSights);
        }

        private async Task<byte[]> ConvertSightToBytes(IFormFile sight)
        {
            byte[] sightBytes;
            var stream = new MemoryStream();

            await sight.CopyToAsync(stream);
            sightBytes = stream.ToArray();

            return sightBytes;
        }

        private Converter<SightEntity, SightModel> converter = ConvertEntityToModel;
        private List<SightModel> ConvertAllToModels(List<SightEntity> sightList)
        {
            return sightList.ConvertAll(converter);
        }
        private static SightModel ConvertEntityToModel(SightEntity s)
        {
            if (s is null)
            {
                return null;
            }

            return new SightModel
            {
                Id = s.Id,
                FileName = s.FileName,
                Data = Convert.ToBase64String(s.Data),
                OversiteId = s.OversiteId
            };
        }
    }
}