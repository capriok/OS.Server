using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OS.Data
{
    class OSContextFactory : IDesignTimeDbContextFactory<OSContext>
    {
            public OSContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<OSContext>();
            //optionsBuilder.UseMySql("Data Source=oversites");

            return new OSContext(optionsBuilder.Options);
            }
    }
}
