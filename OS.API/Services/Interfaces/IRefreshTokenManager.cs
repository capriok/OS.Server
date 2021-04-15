using OS.API.Models.RefreshToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface IRefreshTokenManager
    {
        Task<RefreshTokenModel> GetOneAsync(string token);
        Task<RefreshTokenModel> CreateAsync(RefreshTokenModel token);
        Task<RefreshTokenModel> UpdateAsync(RefreshTokenModel token);
    }
}
