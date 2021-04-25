using OS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities.Interfaces
{
    public interface IUserDomainEntity
    {
        int Id { get; set; }
        string Domain { get; set; }
        int UserId { get; set; }
    }
}
