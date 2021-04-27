using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class UserDomainRepository : IUserDomainRepository
    {
        private readonly ILogger<UserDomainRepository> _Logger;
        private readonly OSContext _OSContext;

        public UserDomainRepository(ILogger<UserDomainRepository> logger, OSContext osContext)
        {
            _Logger = logger;
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
