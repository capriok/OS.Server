using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IUserDomainRepository
    {
        IQueryable<UserDomainEntity> AllDomainsQueryable(); 
        Task<List<UserDomainEntity>> FindByUserIdQuery(int userId);
    }
}