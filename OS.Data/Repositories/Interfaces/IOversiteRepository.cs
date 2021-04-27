using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IOversiteRepository
    {
        IQueryable<OversiteEntity> AllOversitesQueryable();
        Task<List<OversiteEntity>> FindTenRecent();
        Task<List<OversiteEntity>> FindBySearchResult(string searchResult);
        Task<OversiteEntity> FindByIdAsync(int id);
        Task<OversiteEntity> AddOversiteAsync(OversiteEntity oversite);
    }
}