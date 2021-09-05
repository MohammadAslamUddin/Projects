using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Web;
using System.Web.UI.WebControls;

namespace UniversityBITM.Models
{
    [Serializable]
    public class Teacher
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher's Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Teacher's Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Teacher's Email")]
        [RegularExpression(@"^(([^<>()[\]\\.,;:\s@""]+(\.[^<>()[\]\\.,;:\s@""]+)*)|("".+""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$", ErrorMessage = "Email Should be valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter a Contact Number")]
        [RegularExpression(@"^(?:\+88|01)?\d{11}$", ErrorMessage = "Mobile number should be valid")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Designation must be selected")]
        public int Designation { get; set; }
        [Required(ErrorMessage = "Department must be selected")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Credit Required")]
        [Range(0, 100, ErrorMessage = "Credit Should be between 0 to 100")]
        public int CreditToBeTaken { get; set; }
        public int remainingCredit { get; set; }
        
    }
}