using OS.Data.Entities.User;
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
        private readonly OSContext _OSContext;
        public UserRepository(OSContext osContext)
        {
            _OSContext = osContext;
        }

        public IQueryable<UserEntity> GetQueryable()
        {
            return _OSContext.Users.AsQueryable();
        }

        public UserEntity FindByUsername(string username)
        {
            return GetQueryable()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }

        public async Task<UserEntity> FindByIdAsync(int id)
        {
            return await _OSContext.Users.FindAsync(id);
        }

        public async Task<UserEntity> AddAsync(UserEntity user)
        {
            _OSContext.Users.Add(user);
            await _OSContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserEntity> UpdateAsync(UserEntity user)
        {
            var local = _OSContext.Users.Local.FirstOrDefault(entity => entity.Id == user.Id);
            if (local is not null)
            {
                _OSContext.Entry(local).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }

            _OSContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _OSContext.SaveChangesAsync();

            return user;
        }

        public async Task RemoveAsync(int id)
        {
            var user = await _OSContext.Users.FindAsync(id);
            if (user is not null)
            {
                _OSContext.Users.Remove(user);
                await _OSContext.SaveChangesAsync();
            }
        }
    }
}
