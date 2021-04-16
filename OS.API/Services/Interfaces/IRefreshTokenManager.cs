using OS.API.Models.RefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface IRefreshTokenManager
    {
        RefreshTokenModel GetOneByTokenAsync(string token);
        RefreshTokenModel GetOneByUserIdAsync(int userId);
        Task<RefreshTokenModel> CreateAsync(RefreshTokenModel token);
        Task<RefreshTokenModel> UpdateAsync(string oldToken, RefreshTokenModel token);
    }
}
