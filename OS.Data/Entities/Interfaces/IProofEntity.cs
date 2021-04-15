using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities.Interfaces
{
    public interface IProofEntity
    {
        int Id { get; set; }
        string Blob { get; set; }
        int OversiteId { get; set; }
    }
}