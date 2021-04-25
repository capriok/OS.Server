using Microsoft.EntityFrameworkCore;
using OS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data
{
    public class OSContext : DbContext
    {
        public OSContext(DbContextOptions<OSContext> options) : base(options) { }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<UserDomainEntity> UserDomain { get; set; }
        public DbSet<RefreshTokenEntity> RefreshToken{ get; set; }
        public DbSet<OversiteEntity> Oversite { get; set; }
        public DbSet<SightEntity> Sight{ get; set; }
    }
}
