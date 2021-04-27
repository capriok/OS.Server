using OS.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.Data.Entities
{
    public class SightEntity : ISightEntity
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public string FileName { get; set; }
        public int OversiteId { get; set; }
    }
}