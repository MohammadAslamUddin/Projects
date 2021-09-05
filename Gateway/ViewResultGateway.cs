using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.Gateway
{
    public class ViewResultGateway : CommonGateway
    {
        public List<Course> GetResultByStudentId(int id)
        {
            Query =
                "SELECT * FROM StudentResult LEFT OUTER JOIN Grade " +
                "ON StudentResult.grade_id = Grade.grade_id " +
                "left outer join Course" +
                " ON StudentResult.course_id = Course.course_id " +
                "WHERE StudentResult.student_id = @StudentId;";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<Course> courses = new List<Course>();

            while (Reader.Read())
            {
                Course course = new Course();
                course.Code = Reader["course_code"].ToString();
                course.Name = Reader["course_name"].ToString();
                course.Grade = Reader["grade_name"].ToString();

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();
            return courses;
        }
    }
}