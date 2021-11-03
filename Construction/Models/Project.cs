using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Construction.Models
{
    public class PoneNumber
    {
        public int Id { get; set; }
        public string projectName { get; set; }
        public string location { get; set; }
        public int numberOfBuildings { get; set; }
        public int numberOfUnits { get; set; }

        public ICollection<Building> Buildings { get; set; }


    }
}
