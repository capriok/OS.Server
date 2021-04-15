using OS.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities
{
    public class RefreshTokenEntity : IRefreshTokenEntity
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}