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
    public class CookieOps : CookieOptions
    {
        public CookieOps()
        {
            Secure = true;
            HttpOnly = true;
            SameSite = SameSiteMode.None;
            //SameSite = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict
        }
    }

    public class CookieService : ICookieService
    {
        private readonly IConfiguration _Config;
        public CookieService(IConfiguration config)
        {
            _Config = config;
        }

        private readonly string AuthTokenCookie = "Cookie:AuthToken";
        private readonly string RefreshTokenCookie = "Cookie:RefreshToken";


        public void AppendAuthenticationCookie(HttpResponse Response, string accessToken)
        {
            Response.Cookies.Append(_Config[AuthTokenCookie], accessToken, new CookieOps()
            {
                //Expires = DateTimeOffset.Now.AddMinutes(60)
                Expires = DateTimeOffset.Now.AddSeconds(5)
            });
        }

        public void AppendRefreshAuthenticationCookie(HttpResponse Response, string refreshToken)
        {
            Response.Cookies.Append(_Config[RefreshTokenCookie], refreshToken, new CookieOps()
            {
                HttpOnly = false,
                Expires = DateTimeOffset.Now.AddDays(3)
            });
        }

        public void DeleteAuthenticationCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_Config[AuthTokenCookie], new CookieOps());
        }

        public void DeleteRefreshAuthenticationCookie(HttpResponse Response)
        {
            Response.Cookies.Delete(_Config[RefreshTokenCookie], new CookieOps());
        }
    }
}
