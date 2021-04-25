using OS.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Managers.Interfaces
{
    public interface IUserDomainManager
    {
        public Task<List<UserDomainModel>> GetAllByUserId(int userId);
    }
}
