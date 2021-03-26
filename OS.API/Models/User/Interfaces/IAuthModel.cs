using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.User.Interfaces
{
    interface IAuthModel
    {
        string Username { get; set; }
        string Password{ get; set; }
    }
}
