using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.User
{
    public class UserBase : IUserBase
    {
        public int Id { get; set; }
        public string Username{ get; set; }
    }
}
