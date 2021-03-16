using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username{ get; set; }
        public string Password{ get; set; }
        public string JoinDate { get; set; }
    }
}
