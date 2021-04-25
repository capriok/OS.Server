using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities.Interfaces
{
    public interface ISightEntity
    {
        int Id { get; set; }
        string Data { get; set; }
        int OversiteId { get; set; }
    }
}