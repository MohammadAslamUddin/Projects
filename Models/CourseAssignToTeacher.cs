using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projects.Models
{
    public class CourseAssignToTeacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Select a Department")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please Select a Teacher")]
        public int TeacherId { get; set; }
        public int CreditToBeTaken { get; set; }
        public int RemainingCredit { get; set; }
        [Required(ErrorMessage = "Please Select a Course")]
        public int CourseCode { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int CourseCredit { get; set; }
    }
}