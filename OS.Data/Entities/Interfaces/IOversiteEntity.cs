using OS.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities.Interfaces
{
    public interface IOversiteEntity
    {
        int Id { get; set; }
        string Title { get; set; }
        string Domain { get; set; }
        string Description { get; set; }
        string Category{ get; set; }
        string Severity{ get; set; }
        bool Private{ get; set; }
        int UserId { get; set; }
    }
}
