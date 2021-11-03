using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Construction.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public int numberOfRooms { get; set; }
        public int area { get; set; }
        public int BuildingId { get; set; }

        public Building building { get; set; }
    }
}
