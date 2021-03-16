using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OS.Data.Entities;

namespace OS.Data
{
    public class OSContext : DbContext
    {
        public OSContext(DbContextOptions<OSContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<User> Oversites { get; set; }
    }
}
