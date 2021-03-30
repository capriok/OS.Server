using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities.Interfaces
{
    public interface IUserEntity
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        DateTime JoinDate { get; set; }
    }
}
