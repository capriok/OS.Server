using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models
{
    public interface IUserModel : IUserBase
    {
        int Id { get; set; }
        string Username { get; set; }
        DateTime JoinDate { get; set; }
    }
}