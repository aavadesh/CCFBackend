using Microsoft.AspNetCore.Cors;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    [EnableCors("corspolicy")]
    public class EmployeeCompetency
    {
        [Key]
        public int EmployeeCompetencyID { get; set; }
        public int CompetencyID { get; set; }
        public string EmployeeCommnet { get; set; }
        public string ReviewerComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedID { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewID { get; set; }
        public bool IsComplete { get; set; }
    }
}
