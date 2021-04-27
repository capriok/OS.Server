using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface ISightRepository
    {
        IQueryable<SightEntity> AllOversitesQueryable();
        Task<List<SightEntity>> GetOversitesSights(int oversiteId);
        Task<SightEntity> AddSightAsync(SightEntity sight);
        Task<List<SightEntity>> AddSightRangeAsync(List<SightEntity> sights);
    }
}
