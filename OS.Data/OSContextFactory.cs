﻿using System;
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
            //optionsBuilder.UseSqlite("Data Source=todos.db");

            return new OSContext(optionsBuilder.Options);
            }
    }
}
