using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projects.Models
{
    public class EnrollCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        [Display(Name = "Student Reg. No.")]
        public int StudentRegNo { get; set; }
        public int StudentId { get; set; }
        [Display(Name = "Select Course")]
        public int CourseId { get; set; }
        
        [Required(ErrorMessage = "Please Enter Date")]
        public String Date { get; set; }
    }
}