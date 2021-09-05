using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Gateway;
using Projects.Models;
using UniversityBITM.Models;

namespace Projects.BLL
{
    public class EnrollCourseManager
    {
        private EnrollCourseGateway enrollCourseGateway;

        public EnrollCourseManager()
        {
            enrollCourseGateway = new EnrollCourseGateway();
        }

        public List<SelectListItem> GetAllStudents()
        {
            return enrollCourseGateway.GetAllStudents();
        }

        public string Save(EnrollCourse enroll)
        {
            if (enrollCourseGateway.CourseAssignedAlready(enroll))
            {
                return "The course is already assigned";
            }
            else
            {
                int rowAffected = enrollCourseGateway.Save(enroll);
                if (rowAffected>0)
                {
                    return "Saved!";
                }
                else
                {
                    return "Saving Failed!";
                }
            }
        }

        public RegisterStudent GetStudnetInfoByCourseId(int id)
        {
            return enrollCourseGateway.GetStudnetInfoByCourseId(id);
        }

        public List<SelectListItem> GetCoursesByDepId(int id)
        {
            return enrollCourseGateway.GetCoursesByDepId(id);
        }

        public RegisterStudent GetDeptIdByStudentId(int id)
        {
            return enrollCourseGateway.GetDeptIdByStudentId(id);
        }

        public string UnassignCourseStudent()
        {
            int RowAffected = enrollCourseGateway.UnassignCourseStudent();
            if (RowAffected > 0)
            {
                return "Unassigned Courses from student";
            }
            else
            {
                return "Unassigning Courses failed!";
            }
        }
    }
}