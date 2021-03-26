using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using OS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API
{
    public class DbContextInstaller
    {
        public DbContextInstaller(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<OSContext>(
                contextOptions => contextOptions.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 23)),
                    mySqlOptions => mySqlOptions
                        .CharSetBehavior(CharSetBehavior.NeverAppend))
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
            );
        }
    }
}
