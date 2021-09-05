using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Projects.Models;
using UniversityBITM.Gateway;

namespace Projects.Gateway
{
    public class CourseStatisticsGateway : CommonGateway
    {
        public List<CourseStatistics> GetAllCourses(int depId)
        {
            Query = "SELECT Course.course_code Code, Course.course_name Name, Semester.semester_name Semester, ISNULL(Teacher.teacher_name, 'Not Assigned Yet') AssignedTo FROM Course " +
                    "LEFT JOIN CourseAssignTeacher " +
                    "ON Course.course_id = CourseAssignTeacher.course_id " +
                    "LEFT JOIN Teacher " +
                    "ON CourseAssignTeacher.teacher_id = Teacher.teacher_id " +
                    "LEFT JOIN Semester " +
                    "ON Course.semester_id = Semester.semester_id " +
                    "WHERE Course.department_id = @DepartmentId;";

            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = depId;

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<CourseStatistics> list = new List<CourseStatistics>();
            while (Reader.Read())
            {
                CourseStatistics courseStatistics = new CourseStatistics();
                courseStatistics.CourseCode = Reader["Code"].ToString();
                courseStatistics.CourseName = Reader["Name"].ToString();
                courseStatistics.Semester = Reader["Semester"].ToString();
                courseStatistics.Teacher = Reader["AssignedTo"].ToString();

                list.Add(courseStatistics);
            }
            Reader.Close();
            Connection.Close();

            return list;
        }
    }
}