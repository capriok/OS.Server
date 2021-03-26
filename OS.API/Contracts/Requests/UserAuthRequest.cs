using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Requests
{
    public class UserAuthRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
