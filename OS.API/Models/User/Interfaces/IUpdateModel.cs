using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models
{
    public interface IUpdateModel : IUserBase
    {
        int Id { get; set; }
        string Username { get; set; }
        string Password { get; set; }
    }
}