using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Construction.ViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }
        public string projectName { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public int numberOfBuildings { get; set; }
        public int numberOfUnits { get; set; }
        public bool closed { get; set; }
        public IFormFile headerImage { get; set; }
        public IFormFile ProSureImage { get; set; }

    }
}
