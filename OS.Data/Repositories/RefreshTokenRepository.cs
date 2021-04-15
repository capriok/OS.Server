using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly ILogger<RefreshTokenRepository> _Logger;
        private readonly OSContext _OSContext;
        public RefreshTokenRepository(ILogger<RefreshTokenRepository> logger, OSContext osContext)
        {
            _Logger = logger;
            _OSContext = osContext;
        }

        public RefreshTokenEntity FindByToken(string token)
        {
            return _OSContext.RefreshTokens.AsQueryable()
                .Where(t => t.Token.Equals(token))
                //.Where(t => t.UserId.Equals(1))
                .FirstOrDefault();
        }

        public async Task<RefreshTokenEntity> AddAsync(RefreshTokenEntity token)
        {
            await  _OSContext.RefreshTokens.AddAsync(token);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation($"(Repository) Token Added: {token.UserId}");

            return token;
        }

        public async Task<RefreshTokenEntity> UpdateAsync(RefreshTokenEntity token)
        {
            var local = _OSContext.RefreshTokens.Local.FirstOrDefault(entity => entity.Id == token.Id);
            if (local is not null)
            {
                _OSContext.Entry(local).State = EntityState.Detached;
            }

            _OSContext.Entry(token).State = EntityState.Modified;
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation($"(Repository) Token Updated: {token.UserId}");

            return token;
        }
    }
}
