using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class SightRepository : ISightRepository
    {
        private readonly ILogger<SightRepository> _Logger;
        private readonly OSContext _OSContext;

        public SightRepository(ILogger<SightRepository> logger, OSContext osContext)
        {
            _Logger = logger;
            _OSContext = osContext;
        }

        public IQueryable<SightEntity> AllOversitesQueryable()
        {
            return _OSContext.Sight.AsQueryable();
        }

        public async Task<SightEntity> GetSightByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<SightEntity> AddSightAsync(SightEntity sight)
        {
            await _OSContext.Sight.AddAsync(sight);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation($"(Repository) Sight Added For: {sight.OversiteId}");

            return sight;
        }
    }
}
