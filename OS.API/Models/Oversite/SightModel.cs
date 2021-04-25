using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.Oversite
{
    public class SightModel
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string FileName { get; set; }
        public int OversiteId{ get; set; }
    }
}
