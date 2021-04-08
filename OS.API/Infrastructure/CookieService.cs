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
    class CookieOps : CookieOptions
    {
        public CookieOps()
        {
            Secure = true;
            HttpOnly = true;
            SameSite = SameSiteMode.None;
            // SameSite = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict //
        }
    }

    public class CookieService : ICookieService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        public CookieService(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        private readonly string usernameCookie = "Cookie:Username";
        private readonly string AuthTokenCookie = "Cookie:AuthToken";
        private readonly string RefreshTokenCookie = "Cookie:RefreshToken";

        public void AppendUsernameCookie(HttpResponse Response, string username)
        {
            Response.Cookies.Append(_config[usernameCookie], username,new CookieOps());
        }

        public void AppendAuthenticationCookie(HttpResponse Response, string accessToken)
        {
            Response.Cookies.Append(_config[AuthTokenCookie], accessToken,new CookieOps());
        }

        public void AppendRefreshAuthenticationCookie(HttpResponse Response, string refreshToken)
        {
            Response.Cookies.Append(_config[RefreshTokenCookie], refreshToken,new CookieOps());
        }

        public void DeleteUsernameCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_config[usernameCookie],new CookieOps());
        }

        public void DeleteAuthenticationCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_config[AuthTokenCookie],new CookieOps());
        }

        public void DeleteRefreshAuthenticationCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_config[RefreshTokenCookie], new CookieOps());
        }
    }
}
