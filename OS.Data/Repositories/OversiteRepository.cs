using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class OversiteRepository : IOversiteRepository
    {
        private readonly OSContext _OSContext;
        public OversiteRepository(OSContext osContext)
        {
            _OSContext = osContext;
        }

        public IQueryable<OversiteEntity> GetQueryable()
        {
            return _OSContext.Oversites.AsQueryable();
        }

        public async Task<OversiteEntity> FindByIdAsync(int oversiteId)
        {
            return await _OSContext.Oversites.FindAsync(oversiteId);
        }

    }
}
