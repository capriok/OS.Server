using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories.Interfaces
{
    public interface IRefreshTokenRepository
    {
        RefreshTokenEntity FindByToken(string token);
        RefreshTokenEntity FindByUserId(int userId);
        Task<RefreshTokenEntity> AddAsync(RefreshTokenEntity token);
        Task<RefreshTokenEntity> UpdateAsync(RefreshTokenEntity token);
    }
}
