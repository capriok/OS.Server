﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Contracts.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username{ get; set; }
        public DateTime JoinDate{ get; set; }
    }
}