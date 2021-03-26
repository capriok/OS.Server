using OS.API.Models.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.User
{
    public class AuthModel : IAuthModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
