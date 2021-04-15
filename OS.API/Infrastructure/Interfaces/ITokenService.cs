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
        Task IssueAuthenticationTokens(HttpResponse Response, UserModel user);
        Task RevokeAuthenticationRefreshTokens(HttpResponse Response, string oldToken);
    }
}
