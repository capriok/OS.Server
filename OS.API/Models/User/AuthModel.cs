using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.User
{
    public class AuthModel
    {
        public AuthModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
