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
        Task<SightEntity> GetSightByUserId(int userId);
        Task<SightEntity> AddSightAsync(SightEntity sight);
    }
}
