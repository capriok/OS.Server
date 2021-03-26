using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IOversiteRepository
    {
        IQueryable<OversiteEntity> GetQueryable();
        Task<OversiteEntity> FindByIdAsync(int id);
    }
}