using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API
{
    public class CorsInstaller
    {
        public CorsInstaller(IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddCors(o => o.AddPolicy(Configuration["Cors:Policy"], builder =>
            {
                builder
                .WithOrigins(Configuration["Cors:Origins"])
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
            }));
        }
    }
}
