using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OS.Data.Entities;
using OS.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly OSContext _osContext;
        public UserRepository(ILogger<UserRepository> logger, OSContext osContext)
        {
            _logger = logger;
            _osContext = osContext;
        }

        public IQueryable<UserEntity> GetQueryable()
        {
            return _osContext.Users.AsQueryable();
        }

        public UserEntity FindByUsername(string username)
        {
            return GetQueryable()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }

        public async Task<UserEntity> FindByIdAsync(int id)
        {
            return await _osContext.Users.FindAsync(id);
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            _osContext.Users.Add(user);
            await _osContext.SaveChangesAsync();

            _logger.LogInformation($"(Repository) User Added: {user.Id}");

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            var local = _osContext.Users.Local.FirstOrDefault(entity => entity.Id == user.Id);
            if (local is not null)
            {
                _osContext.Entry(local).State = EntityState.Detached;
            }

            _osContext.Entry(user).State = EntityState.Modified;
            await _osContext.SaveChangesAsync();

            _logger.LogInformation($"(Repository) User Updated: {user.Id}");

            return user;
        }

        public async Task RemoveAsync(int id)
        {
            var user = await _osContext.Users.FindAsync(id);
            if (user is not null)
            {
                _osContext.Users.Remove(user);
                await _osContext.SaveChangesAsync();
            }
            _logger.LogInformation($"(Repository) User Removed: {id}");
        }
    }
}
