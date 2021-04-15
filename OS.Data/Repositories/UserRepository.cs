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
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _Logger;
        private readonly OSContext _OSContext;
        public UserRepository(ILogger<UserRepository> logger, OSContext osContext)
        {
            _Logger = logger;
            _OSContext = osContext;
        }

        public IQueryable<UserEntity> GetQueryable()
        {
            return _OSContext.Users.AsQueryable();
        }

        public UserEntity FindByUsername(string username)
        {
            return _OSContext.Users.AsQueryable()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }

        public async Task<UserEntity> FindByIdAsync(int id)
        {
            return await _OSContext.Users.FindAsync(id);
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            await _OSContext.Users.AddAsync(user);
            await _OSContext.SaveChangesAsync();

            _Logger.LogInformation($"(Repository) User Added: {user.Id}");

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            var local = _OSContext.Users.Local.FirstOrDefault(entity => entity.Id == user.Id);
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
            var user = await _OSContext.Users.FindAsync(userId);
            if (user is not null)
            {
                _OSContext.Users.Remove(user);
                await _OSContext.SaveChangesAsync();
            }
            _Logger.LogInformation($"(Repository) User Removed: {userId}");
        }
    }
}
