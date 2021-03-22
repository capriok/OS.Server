using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models
{
    public class PutUserScaffold
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string JoinDate { get; set; }
    }
}
