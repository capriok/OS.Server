using Microsoft.AspNetCore.Http;
using OS.API.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Infrastructure.Interfaces
{
    public interface ITokenService
    {
        void GrantAuthorizationTokens(HttpResponse Response, UserModel user);
        void RevokeAuthorizationTokens(HttpResponse Response, UserModel user);
    }
}
