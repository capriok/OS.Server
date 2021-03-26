using OS.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJWT(UserModel user);
    }
}
