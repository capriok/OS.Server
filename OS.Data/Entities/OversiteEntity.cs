using OS.Data.Entities.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.Data.Entities
{
    public class OversiteEntity : IOversiteEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Website { get; set; }
        public string Category { get; set; }
        public string Severity { get; set; }
    }
}