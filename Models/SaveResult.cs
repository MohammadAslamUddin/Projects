using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projects.Models
{
    public class SaveResult
    {
        public int Id { get; set; }
        public string StudentRegNo { get; set; }
        public int StudentId  { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int Grades { get; set; }
    }
}