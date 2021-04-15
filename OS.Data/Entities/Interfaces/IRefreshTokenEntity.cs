using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities.Interfaces
{
    public interface IRefreshTokenEntity
    {
        int Id { get; set; }
        string Token{ get; set; }
        int UserId { get; set; }
    }
}