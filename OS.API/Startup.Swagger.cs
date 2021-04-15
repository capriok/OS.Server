using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API
{
    public class SwaggerInstaller
    {
        public SwaggerInstaller(IServiceCollection Services, IConfiguration Configuration)
        {
            Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Oversites API", Version = "v1" });
            });
        }
    }
}
