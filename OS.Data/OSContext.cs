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

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens{ get; set; }
        public DbSet<OversiteEntity> Oversites { get; set; }
        public DbSet<ProofEntity> Proofs{ get; set; }
    }
}
