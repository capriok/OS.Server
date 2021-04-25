using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OS.API.Models.Oversite
{
    public class OversiteFormData
    {
        public string Title { get; set; }
        public string Domain { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
        public string Private { get; set; }
        public IFormFileCollection Sights {get;set;}
        public string UserId { get; set; }
    }
}
