using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Requests
{
    public class UpdateEntityRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
    }
}
