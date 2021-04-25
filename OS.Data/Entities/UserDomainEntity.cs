using OS.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities
{
    public class UserDomainEntity :  IUserDomainEntity
    {
        public int Id { get; set; }
        public string Domain { get; set; }
        public int UserId { get; set; }
    }
}
