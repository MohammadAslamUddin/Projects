using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace UniversityBITM.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter a Course Code")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Course Code Must be at least 5 characters")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Please Enter a Course Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Course Credit Required")]
        [Range(0.0,5.0)]
        public float Credit { get; set; }
        [Required(ErrorMessage = "Description needed")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Select A Department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        [Required(ErrorMessage = "Semester Must be selected")]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        public string Grade { get; set; }
    }
}