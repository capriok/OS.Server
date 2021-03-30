using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Responses.User
{
    public class AuthReponse
    {
        public int User { get; set; }
        public DateTime LastLogin{ get; set; }
    }
}
