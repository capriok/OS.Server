using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Infrastructure.Interfaces
{
    public interface ICookieService
    {
        void AppendAuthenticationCookie(HttpResponse Response, string accessToken);
        void AppendRefreshAuthenticationCookie(HttpResponse Response, string refreshToken);
        void DeleteAuthenticationCookie(HttpResponse Response);
        void DeleteRefreshAuthenticationCookie(HttpResponse Response);
    }
}
