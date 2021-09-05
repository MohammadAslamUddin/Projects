using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityBITM.Models;

namespace Projects.Models
{
    public class DownloadModel
    {
        public RegisterStudent Student { get; set; }
        public List<Course> Courses { get; set; }
    }
}