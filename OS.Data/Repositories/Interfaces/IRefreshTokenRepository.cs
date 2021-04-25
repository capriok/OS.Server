using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        IQueryable<RefreshTokenEntity> AllRefreshTokensQueryable();
        Task<RefreshTokenEntity> FindByToken(string token);
        Task<RefreshTokenEntity> FindByUserId(int userId);
        Task<RefreshTokenEntity> AddAsync(RefreshTokenEntity token);
        Task<RefreshTokenEntity> UpdateAsync(RefreshTokenEntity token);
    }
}
