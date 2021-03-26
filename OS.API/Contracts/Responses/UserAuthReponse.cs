using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Responses
{
    public class UserAuthReponse
    {
        public string Token { get; set; }
        public string Message{ get; set; }
        public bool IsAuth { get; set; }
    }
}
