using OS.Data.Interfaces;
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

        public IQueryable<Entities.User> GetQueryable()
        {
            return _OSContext.Users.AsQueryable();
        }

        public async Task<Entities.User> FindByIdAsync(int id)
        {
            return await _OSContext.Users.FindAsync(id);
        }

        public Entities.User FindByUsername(string username)
        {
            return GetQueryable()
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }

        public async Task<Entities.User> AddAsync(Entities.User user)
        {
            _OSContext.Users.Add(user);
            await _OSContext.SaveChangesAsync();

            return user;
        }

        public async Task<Entities.User> UpdateAsync(Entities.User user)
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
