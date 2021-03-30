using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using OS.API.Services.Interfaces;
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Services
{
    public class CookieService : ICookieService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        public CookieService(IWebHostEnvironment env, IConfiguration config)
        {
            _env = env;
            _config = config;
        }

        public CookieOptions cookieOptions = new CookieOptions()
        {
            Secure = true,
            HttpOnly = true,
            SameSite = SameSiteMode.None
        };
        //{ Secure = true, HttpOnly = true, SameSite = _env.IsDevelopment() ? SameSiteMode.None : SameSiteMode.Strict };

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
    }
}
