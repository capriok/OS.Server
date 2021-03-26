using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Requests.User
{
    public class UpdateRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
