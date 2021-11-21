using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Construction.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string projectName { get; set; }
        public string description { get; set; }

        public string location { get; set; }
        public int numberOfBuildings { get; set; }
        public int numberOfUnits { get; set; }
        public bool closed { get; set; }
        public string headerImage { get; set; }
        public string ProsureImage { get; set; }

        public ICollection<Building> Buildings { get; set; }


    }
}
