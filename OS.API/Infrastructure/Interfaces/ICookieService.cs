using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Infrastructure.Interfaces
{
    public interface ICookieService
    {
        void AppendUsernameCookie(HttpResponse Response, string username);
        void AppendAuthenticationCookie(HttpResponse Response, string accessToken);
        void AppendRefreshAuthenticationCookie(HttpResponse Response, string refreshToken);
        void DeleteUsernameCookie(HttpResponse Response);
        void DeleteAuthenticationCookie(HttpResponse Response);
        void DeleteRefreshAuthenticationCookie(HttpResponse Response);
    }
}
