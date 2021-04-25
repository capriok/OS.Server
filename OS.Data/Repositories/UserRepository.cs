using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _Logger;
        private readonly OSContext _OSContext;

        public UserRepository(ILogger<UserRepository> logger, OSContext osContext)
        {
            _Logger = logger;
            _OSContext = osContext;
        }

        public IQueryable<UserEntity> AllUsersQueryable()
        {
            return _OSContext.User.AsQueryable();
        }

        public async Task<UserEntity> FindByUsername(string username)
        {
            return await AllUsersQueryable()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefaultAsync();
        }

        public async Task<UserEntity> FindByIdAsync(int id)
        {
            return await _OSContext.User.FindAsync(id);
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            await _OSContext.User.AddAsync(user);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation($"(Repository) User Added: {user.Username}");

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            var local = _OSContext.User.Local.FirstOrDefault(entity => entity.Id == user.Id);
            if (local is not null)
            {
                _OSContext.Entry(local).State = EntityState.Detached;
            }

            _OSContext.Entry(user).State = EntityState.Modified;
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation($"(Repository) User Updated: {user.Id}");

            return user;
        }

        public async Task RemoveAsync(int userId)
        {
            var user = await _OSContext.User.FindAsync(userId);
            if (user is not null)
            {
                _OSContext.User.Remove(user);
                await _OSContext.SaveChangesAsync();
            }
            _Logger.LogInformation($"(Repository) User Removed: {userId}");
        }
    }
}
