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
    public class CourseGateway : CommonGateway
    {
        public int Save(Course course)
        {
            Query = "INSERT INTO Course VALUES(@code,@name,@credit,@desc,@depId, @semId);";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("code",SqlDbType.VarChar);
            Command.Parameters["code"].Value = course.Code;
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = course.Name;
            Command.Parameters.Add("credit", SqlDbType.Float);
            Command.Parameters["credit"].Value = course.Credit;
            Command.Parameters.Add("desc", SqlDbType.VarChar);
            Command.Parameters["desc"].Value = course.Description;
            Command.Parameters.Add("depId", SqlDbType.Int);
            Command.Parameters["depId"].Value = course.DepartmentId;
            Command.Parameters.Add("semId", SqlDbType.Int);
            Command.Parameters["semId"].Value = course.SemesterId;
            
            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        public bool IsCodeExist(string code)
        {
            Query = "SELECT * FROM Course WHERE course_code = '" + code + "';";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();

            Reader = Command.ExecuteReader();

            bool hasRows = false;
            if (Reader.HasRows)
            {
                hasRows = true;
            }
            Reader.Close();
            Connection.Close();

            return hasRows;
        }
        public List<Course> GetCoursebyDepartmentId(int id)
        {
            Query = "SELECT * FROM Course WHERE department_id=@id";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id = (int)Reader["course_id"];
                course.Code = Reader["course_code"].ToString();
                course.Name = Reader["course_name"].ToString();
                course.Credit = float.Parse(Reader["course_credit"].ToString());

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();

            return courses;
        }
        public Course GetCourseNameByCourseId(int id)
        {
            Query = "SELECT * FROM Course WHERE course_id=@id";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            Course course = new Course();
            while (Reader.Read())
            {
                course.Name = Reader["course_name"].ToString();
                course.Credit = float.Parse(Reader["course_credit"].ToString());
            }
            Reader.Close();
            Connection.Close();

            return course;
        }
    }
}