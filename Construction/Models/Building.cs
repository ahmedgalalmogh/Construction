using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Construction.Models
{
    public class Building
    {
        public int Id { get; set; }
        public int numberOfUnits { get; set; }
        public int numberOfFloors { get; set; }
        
        public int projectId { get; set; }
        public Project project { get; set; }

        public ICollection<Unit> units { get; set; }

    }
}
