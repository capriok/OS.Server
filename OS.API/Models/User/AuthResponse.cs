using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.User
{
    public class AuthResponse
    {
        public AuthResponse(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string LastLogin { get; set; }
        public DateTime JoinDate { get; set; }
        public List<UserDomainModel> Domains { get; set; }
    }
}
