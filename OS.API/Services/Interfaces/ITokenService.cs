using Microsoft.AspNetCore.Http;
using OS.API.Contracts.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface ITokenService
    {
        void GrantAuthorizationTokens(HttpResponse Response, UserModel user);
    }
}
