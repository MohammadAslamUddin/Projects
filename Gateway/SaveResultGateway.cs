using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using Projects.Models;
using UniversityBITM.Gateway;

namespace Projects.Gateway
{
    public class SaveResultGateway : CommonGateway
    {
        public List<SelectListItem> GetAllGrades()
        {
            Query = "SELECT * FROM Grade;";
            Command = new SqlCommand(Query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<SelectListItem> grades = new List<SelectListItem>();

            while (Reader.Read()) 
            {
                SelectListItem grade = new SelectListItem();
                grade.Value = Reader["grade_id"].ToString();
                grade.Text = Reader["grade_name"].ToString();

                grades.Add(grade);
            }
            Reader.Close();
            Connection.Close();

            return grades;
        }

        public bool IsResultExist(SaveResult result)
        {
            Query = "SELECT * FROM StudentResult WHERE student_id = @StudentId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = result.StudentRegNo;

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

        public int UpdateResult(SaveResult result)
        {
            Query = "UPDATE StudentResult SET grade_id = @Grade WHERE student_id = @StudentId AND course_id = @CourseId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = result.StudentRegNo;

            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = result.CourseId;

            Command.Parameters.Add("Grade", SqlDbType.Int);
            Command.Parameters["Grade"].Value = result.Grades;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        public int Save(SaveResult result)
        {
            Query = "INSERT INTO StudentResult VALUES(@StudentId, @CourseId, @Grade);";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = result.StudentRegNo;

            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = result.CourseId;

            Command.Parameters.Add("Grade", SqlDbType.Int);
            Command.Parameters["Grade"].Value = result.Grades;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }
    }
}