using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.ViewModels
{
    public class EmployeeCompetencyViewModel
    {
        public int EmployeeCompetencyID { get; set; }
        public int CompetencyID { get; set; }
        public string EmployeeCommnet { get; set; }
        public string ReviewerComment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedID { get; set; }
        public DateTime ReviewDate { get; set; }
        public int ReviewID { get; set; }
        public bool IsComplete { get; set; }

        public bool IsSave { get; set; }
        public bool IsDraft { get; set; }

        public string FileName { get; set; }
        [NotMapped]
        public IFormFile[] Files { get; set; }

        public int CompetencyFrameworkID { get; set; }
        public int EmployeeID { get; set; }
        public string CompetencyName { get; set; }
        public string FrameworkName { get; set; }

    }
}
