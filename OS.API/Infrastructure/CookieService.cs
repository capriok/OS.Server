using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using OS.API.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Infrastructure
{
    public class CookieService : ICookieService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly CookieOptions cookieOptions = new ()
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
            //SameSite = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict
        };
        public CookieService(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public void AppendUsernameCookie(HttpResponse Response, string username)
        {
            Response.Cookies.Append(_config["Cookie:Username"], username, cookieOptions);
        }

        public void AppendAuthorizationCookie(HttpResponse Response, string accessToken)
        {
            Response.Cookies.Append(_config["Cookie:AuthToken"], accessToken, cookieOptions);
        }

        public void AppendAuthorizationRefreshCookie(HttpResponse Response, string refreshToken)
        {
            Response.Cookies.Append(_config["Cookie:RefreshToken"], refreshToken, cookieOptions);
        }

        public void DeleteUsernameCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_config["Cookie:AuthTokenUsernameToken"]);
        }

        public void DeleteAuthorizationCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_config["Cookie:AuthToken"]);
        }

        public void DeleteRefreshAuthorizationCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_config["Cookie:RefreshToken"]);
        }
    }
}
