using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OS.Data.Interfaces;
using OS.Data.Entities;

namespace OS.Data.Repositories
{
    class UserRepository : IUserRepository
    {
        private readonly OSContext _OSContext;
        public UserRepository (OSContext osContext)
        {
            _OSContext = osContext;
        }

        public Task<User> Add(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Find(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> Get()
        {
            throw new NotImplementedException();
        }

        public Task<User> Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
