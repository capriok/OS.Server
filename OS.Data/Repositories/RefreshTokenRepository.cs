using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System.Linq;
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

        public IQueryable<RefreshTokenEntity> AllRefreshTokensQueryable()
        {
            return _OSContext.RefreshToken.AsQueryable();
        }

        public async Task<RefreshTokenEntity> FindByToken(string token)
        {
            return await AllRefreshTokensQueryable()
                .Where(rt => rt.Token.Equals(token))
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshTokenEntity> FindByUserId(int userId)
        {
            return await AllRefreshTokensQueryable()
                .Where(rt => rt.UserId.Equals(userId))
                .FirstOrDefaultAsync();
        }

        public async Task<RefreshTokenEntity> AddAsync(RefreshTokenEntity token)
        {
            await _OSContext.RefreshToken.AddAsync(token);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation("(Repository) Token Added: {1}", token.UserId);

            return token;
        }

        public async Task<RefreshTokenEntity> UpdateAsync(RefreshTokenEntity token)
        {
            var local = _OSContext.RefreshToken.Local.FirstOrDefault(entity => entity.Id == token.Id);
            if (local is not null)
            {
                _OSContext.Entry(local).State = EntityState.Detached;
            }

            _OSContext.Entry(token).State = EntityState.Modified;
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation("(Repository) Token Updated: {1}", token.UserId);

            return token;
        }
    }
}
