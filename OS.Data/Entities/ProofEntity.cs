using OS.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities
{
    public class ProofEntity : IProofEntity
    {
        public int Id { get; set; }
        public string Blob { get; set; }
        public int OversiteId { get; set; }
    }
}