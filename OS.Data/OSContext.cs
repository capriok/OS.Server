using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OS.Data;

namespace OS.Data
{
    public class OSContext : DbContext
    {
        public OSContext(DbContextOptions<OSContext> options) : base(options) { }

        public DbSet<Entities.User> Users { get; set; }
    }
}
