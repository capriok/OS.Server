using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<SightEntity>> GetOversitesSights(int oversiteId)
        {
            return await AllOversitesQueryable()
                .Where(s => s.OversiteId.Equals(oversiteId))
                .ToListAsync();
        }

        public async Task<SightEntity> AddSightAsync(SightEntity sight)
        {
            await _OSContext.Sight.AddAsync(sight);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation("(Repository) Sight Added For: {1}", sight.OversiteId);

            return sight;
        }

        public async Task<List<SightEntity>> AddSightRangeAsync(List<SightEntity> sights)
        {
            await _OSContext.Sight.AddRangeAsync(sights);
            await _OSContext.SaveChangesAsync();

            foreach (var sight in sights)
            {
                _Logger.LogInformation("(Repository) Sight Added For: {1}", sight.OversiteId);
            }

            return sights;
        }
    }
}
