using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.User
{
    public class AuthResponse
    {
        public AuthResponse(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
        public string LastLogin { get; set; }
    }
}
