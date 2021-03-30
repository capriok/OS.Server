using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services.Interfaces
{
    public interface ICookieService
    {
        void AppendUsernameCookie(HttpResponse Response, string username);
        void AppendAuthorizationCookie(HttpResponse Response, string accessToken);
        void AppendAuthorizationRefreshCookie(HttpResponse Response, string refreshToken);
    }
}
