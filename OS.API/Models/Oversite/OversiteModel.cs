using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.Oversite
{
    public class OversiteModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
        public bool Private{ get; set; }
        public List<SightModel> Sights { get; set; }
        public string Founder { get; set; }
        public int UserId { get; set; }
    }
}