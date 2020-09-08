using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [EnableCors("corspolicy")]
    public class CompetencyFramework
    {
        public int CompetencyFrameworkID { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
