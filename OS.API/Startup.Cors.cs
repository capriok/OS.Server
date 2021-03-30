using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API
{
    public class CorsInstaller
    {
        public CorsInstaller(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddCors(o => o.AddPolicy(Configuration["Cors:Policy"], builder =>
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
