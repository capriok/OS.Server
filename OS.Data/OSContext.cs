using Microsoft.EntityFrameworkCore;
using OS.Data.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data
{
    public class OSContext : DbContext
    {
        public OSContext(DbContextOptions<OSContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
    }
}
