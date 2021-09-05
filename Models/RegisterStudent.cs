using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projects.Models
{
    public class RegisterStudent
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter a Student Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter a Email")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Email Should be valid")]
        public string Email { get; set; }
        [Display(Name = "Contact")]
        [RegularExpression(@"^(?:\+88|01)?\d{11}$", ErrorMessage = "Mobile number should be valid")]
        [Required(ErrorMessage = "Please Enter student Contact Number")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please Enter student Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please select Department")]
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string StudentRegNo { get; set; }
    }
}