using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Projects.Models;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.Gateway
{
    public class CourseAssignToTeacherGateway : CommonGateway
    {
        public int Save(CourseAssignToTeacher courseAssignToTeacher, double credit)
        {
            Query = "INSERT INTO CourseAssignTeacher VALUES(@departmentId, @teacherId, @courseId)";
            string query = "UPDATE Teacher SET teacher_remaincredit = @Value WHERE teacher_id = @teacherId";
            Command = new SqlCommand(Query, Connection);
            SqlCommand command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = courseAssignToTeacher.DepartmentId;
            Command.Parameters.Add("teacherId", SqlDbType.Int);
            Command.Parameters["teacherId"].Value = courseAssignToTeacher.TeacherId;
            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = courseAssignToTeacher.CourseCode;
            command.Parameters.Add("Value", SqlDbType.Float);
            command.Parameters["Value"].Value = credit;
            command.Parameters.Add("teacherId", SqlDbType.Int);
            command.Parameters["teacherId"].Value = courseAssignToTeacher.TeacherId;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();
            int rowAffected = command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected*RowAffected ;
        }

        public bool CourseAssigned(CourseAssignToTeacher courseAssignToTeacher)
        {
            Query = "SELECT * FROM CourseAssignTeacher WHERE department_id = @DepartmentId AND course_id = @CourseId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = courseAssignToTeacher.DepartmentId;
            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = courseAssignToTeacher.CourseCode;

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

        public int UnallocateTeacher()
        {
            Query = "DELETE FROM CourseAssignTeacher";
            string Query1 = "Update Teacher SET teacher_remaincredit = teacher_totalcredit";
            Command = new SqlCommand(Query, Connection);
            SqlCommand command = new SqlCommand(Query1, Connection);

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();
            int rowAffected1 = command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected * rowAffected1;
        }
    }
}