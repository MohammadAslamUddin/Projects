using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projects.Gateway;
using Projects.Models;
using UniversityBITM.Models;

namespace Projects.BLL
{
    public class CourseAssignToTeacherManager
    {
        CourseAssignToTeacherGateway courseAssignToTeacherGateway;
        CourseGateway courseGateway;
        TeacherGateway teacherGateway;

        public CourseAssignToTeacherManager()
        {
            courseAssignToTeacherGateway = new CourseAssignToTeacherGateway();
            courseGateway = new CourseGateway();
            teacherGateway = new TeacherGateway();
        }

        public List<Teacher> GetTeacherbyDepartmentId(int id)
        {
            return teacherGateway.GetTeacherbyDepartmentId(id);
        }

        public List<Course> GetCoursebyDepartmentId(int id)
        {
            return courseGateway.GetCoursebyDepartmentId(id);
        }

        public Teacher GetCreditByTeacherId(int id)
        {
            return teacherGateway.GetCreditByTeacherId(id);
        }

        public Course GetCourseNameByCourseId(int id)
        {
            return courseGateway.GetCourseNameByCourseId(id);
        }
            
        public string Save(CourseAssignToTeacher courseAssignToTeacher)
        {
            if (courseAssignToTeacherGateway.CourseAssigned(courseAssignToTeacher))
            {
                return "Course already Assigned!";
            }
            else
            {
                double remainCredit = Convert.ToSingle(teacherGateway.GetCreditByTeacherId(courseAssignToTeacher.TeacherId).remainingCredit);
                double courseCredit = Convert.ToSingle(courseGateway.GetCourseNameByCourseId(courseAssignToTeacher.CourseCode).Credit);
                double theCredit = remainCredit - courseCredit;
                if (remainCredit < courseCredit)
                {
                    return "Remain Credit must be greater than course Credit";
                }
                else
                {
                    int rowAffected = courseAssignToTeacherGateway.Save(courseAssignToTeacher, theCredit);
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
        }

        public string UnallocateTeacher()
        {
            int rowAffected = courseAssignToTeacherGateway.UnallocateTeacher();
            if (rowAffected>0)
            {
                return "UnAssigned Courses";
            }
            else
            {
                return "UnAssigning Failed!";
            }
        }
    }
}