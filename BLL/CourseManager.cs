using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Gateway;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.BLL
{
    public class CourseManager
    {
        CourseGateway courseGateway = new CourseGateway();

        public string Save(Course course)
        {
            if (courseGateway.IsCodeExist(course.Code))
            {
                return "Please Enter an unique Course Code!";
            }
            else
            {
                int rowAffected = courseGateway.Save(course);
                if (rowAffected > 0)
                    return "Saved!";
                else
                {
                    return "Saving Failed!";
                }
            }
            
        }

        public List<SelectListItem> GetAllSemesters()
        {
            List<SelectListItem> semesterItems = new List<SelectListItem>()
            {
                new SelectListItem(){Value = "1",Text = "1st"},
                new SelectListItem(){Value = "2",Text = "2nd"},
                new SelectListItem(){Value = "3",Text = "3rd"},
                new SelectListItem(){Value = "4",Text = "4th"},
                new SelectListItem(){Value = "5",Text = "5th"},
                new SelectListItem(){Value = "6",Text = "6th"},
                new SelectListItem(){Value = "7",Text = "7th"},
                new SelectListItem(){Value = "8",Text = "8th"}
            };
            return semesterItems;
        }
    }
}