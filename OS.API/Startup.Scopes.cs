using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OS.API.Infrastructure.Interfaces;
using OS.API.Infrastructure;
using OS.API.Services.Interfaces;
using OS.API.Services;
using OS.Data.Repositories.Interfaces;
using OS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.API
{
    public class ScopeInstaller
    {
        public ScopeInstaller(IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddScoped<ITokenService, TokenService>();
            Services.AddScoped<ICookieService, CookieService>();
            Services.AddScoped<IDateService, DateService>();
            Services.AddScoped<IUserManager, UserManager>();
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IOversiteManager, OversiteManager>();
            Services.AddScoped<IOversiteRepository, OversiteRepository>();
            Services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();
            Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        }
    }
}
