using Microsoft.EntityFrameworkCore;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class UserDomainRepository : IUserDomainRepository
    {
        private readonly OSContext _OSContext;

        public UserDomainRepository(OSContext osContext)
        {
            _OSContext = osContext;
        }

        public IQueryable<UserDomainEntity> AllDomainsQueryable()
        {
            return _OSContext.UserDomain.AsQueryable();
        }

        public async Task<List<UserDomainEntity>> FindByUserIdQuery(int userId)
        {
            return await AllDomainsQueryable()
                .Where(d => d.UserId == userId)
                .ToListAsync();
        }

    }
}
