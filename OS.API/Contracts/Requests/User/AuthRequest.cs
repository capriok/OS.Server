using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Requests.User
{
    public class AuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
