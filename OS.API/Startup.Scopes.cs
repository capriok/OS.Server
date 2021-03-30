using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OS.Data.Repositories.Interfaces;
using OS.Data.Repositories;
using OS.API.Services.Interfaces;
using OS.API.Services;
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOversiteService, OversiteService>();
            services.AddScoped<IOversiteRepository, OversiteRepository>();
        }
    }
}
