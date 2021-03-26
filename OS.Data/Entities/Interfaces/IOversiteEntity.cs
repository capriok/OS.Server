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
        int UserId { get; set; }
        string Title { get; set; }
        string Website{ get; set; }
        string Category{ get; set; }
    }
}
