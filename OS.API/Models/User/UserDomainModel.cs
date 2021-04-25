using OS.Data.Entities;
using System;

namespace OS.API.Models.User
{
    public class UserDomainModel
    {
        public UserDomainModel(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public string Domain { get; set; }
        public int UserId { get; set; }
    }
}