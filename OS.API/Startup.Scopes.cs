using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OS.API.Infrastructure.Interfaces;
using OS.API.Infrastructure;
using OS.API.Managers.Interfaces;
using OS.API.Managers;
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
            
            Services.AddScoped<IRefreshTokenManager, RefreshTokenManager>();
            Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            Services.AddScoped<IUserManager, UserManager>();
            Services.AddTransient<IUserRepository, UserRepository>();
            
            Services.AddScoped<IUserDomainManager, UserDomainManager>();
            Services.AddTransient<IUserDomainRepository, UserDomainRepository>();

            Services.AddScoped<IOversiteManager, OversiteManager>();
            Services.AddTransient<IOversiteRepository, OversiteRepository>();
            
            Services.AddScoped<ISightManager, SightManager>();
            Services.AddTransient<ISightRepository, SightRepository>();
        }
    }
}
