using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class OversiteRepository : IOversiteRepository
    {
        private readonly ILogger<OversiteRepository> _Logger;
        private readonly OSContext _OSContext;
        public OversiteRepository(ILogger<OversiteRepository> logger, OSContext osContext)
        {
            _Logger = logger;
            _OSContext = osContext;
        }

        public IQueryable<OversiteEntity> AllOversitesQueryable()
        {
            return _OSContext.Oversite.AsQueryable();
        }
        public async Task<List<OversiteEntity>> FindTenRecent()
        {
            return await AllOversitesQueryable()
                .Select(os => os)
                .Take(10)
                .ToListAsync();
        }

        public async Task<List<OversiteEntity>> FindBySearchResult(string searchResult)
        {
            return await AllOversitesQueryable()
                .Where(o => o.Domain.Equals(searchResult))
                .ToListAsync();
        }

        public async Task<OversiteEntity> FindByIdAsync(int oversiteId)
        {
            return await _OSContext.Oversite.FindAsync(oversiteId);
        }

        public async Task<OversiteEntity> AddOversiteAsync(OversiteEntity oversite)
        {
            await _OSContext.Oversite.AddAsync(oversite);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation("(Repository) Oversite Added: {1}", oversite.Id);

            return oversite;
        }
    }
}
