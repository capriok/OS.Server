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
        public ScopeInstaller(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICookieService, CookieService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOversiteManager, OversiteManager>();
            services.AddScoped<IOversiteRepository, OversiteRepository>();
        }
    }
}
