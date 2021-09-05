using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityBITM.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter a Department Code")]
        [StringLength(7, MinimumLength = 2, ErrorMessage = "Code must be between 2 to 7")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Enter a Department Name")]
        public string Name { get; set; }
    }
}