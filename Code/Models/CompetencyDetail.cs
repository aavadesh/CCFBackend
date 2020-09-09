using Microsoft.AspNetCore.Cors;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    [EnableCors("corspolicy")]
    public class CompetencyDetail
    {
        [Key]
        public int CompetencyID { get; set; }
        public int CompetencyFrameworkID { get; set; }
        public string CompetencyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
