using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.RefreshToken
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
