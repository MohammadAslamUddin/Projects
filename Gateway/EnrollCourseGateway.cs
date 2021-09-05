using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Models;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.Gateway
{
    public class EnrollCourseGateway : CommonGateway
    {
        public List<SelectListItem> GetAllStudents()
        {
            Query = "SELECT * FROM Student";
            Command = new SqlCommand(Query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<SelectListItem> studentsList = new List<SelectListItem>();
            while (Reader.Read())
            {
                SelectListItem student = new SelectListItem();
                student.Value = Reader["student_id"].ToString();
                student.Text = Reader["student_regno"].ToString();

                studentsList.Add(student);
            }

            Reader.Close();
            Connection.Close();

            return studentsList;
        }

        public RegisterStudent GetStudnetInfoByCourseId(int id)
        {
            Query = "SELECT S.student_regno, S.student_address, S.student_id, S.student_name, S.student_email, ISNULL(D.department_name, 'Not Set') department_name,Convert(int ,S.department_id) department_id FROM Student AS S LEFT OUTER JOIN Department AS D ON S.department_id = D.department_id WHERE S.student_id = 18";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("studentId", SqlDbType.Int);
            Command.Parameters["studentId"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            RegisterStudent student = new RegisterStudent();
            while (Reader.Read())
            {
                student.Id = (int) Reader["student_id"];
                student.StudentRegNo = Reader["student_regno"].ToString();
                student.Name = Reader["student_name"].ToString();
                student.Email = Reader["student_email"].ToString();
                student.Address = Reader["student_address"].ToString();
                student.DepartmentId =(int) Reader["department_id"];
                student.DepartmentName = Reader["department_name"].ToString();
            }
            Reader.Close();
            Connection.Close();

            return student;
        }

        public List<SelectListItem> GetCoursesByDepId(int id)
        {
            Query = "SELECT * FROM Course WHERE department_id = @DepartmentId;";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<SelectListItem> courses = new List<SelectListItem>();

            while (Reader.Read())
            {
                SelectListItem course = new SelectListItem();
                course.Value = Reader["course_id"].ToString();
                course.Text = Reader["course_code"].ToString();

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();

            return courses;
        }

        public bool CourseAssignedAlready(EnrollCourse enroll)
        {
            Query = "SELECT * FROM CourseAssignStudent left outer join Student " +
                    "on CourseAssignStudent.student_id = Student.student_id " +
                    "WHERE CourseAssignStudent.course_id = @CourseId AND Student.student_regno = @RegNo";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("RegNo", SqlDbType.VarChar);
            Command.Parameters["RegNo"].Value = enroll.StudentRegNo;
            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = enroll.CourseId;

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

        public int Save(EnrollCourse enroll)
        {
            Query = "INSERT INTO CourseAssignStudent VALUES(@StudentId, @CourseId, @date);";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = enroll.StudentRegNo;

            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = enroll.CourseId;

            Command.Parameters.Add("date", SqlDbType.DateTime);
            Command.Parameters["date"].Value = enroll.Date;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        public RegisterStudent GetDeptIdByStudentId(int id)
        {
            Query = "SELECT * FROM Student WHERE student_id = @StudentId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("StudentId", SqlDbType.Int);
            Command.Parameters["StudentId"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            RegisterStudent student = new RegisterStudent();
            while (Reader.Read())
            {
                student.DepartmentId = (int) Reader["department_id"];
            }
            Reader.Close();
            Connection.Close();

            return student;
        }

        public int UnassignCourseStudent()
        {
            Query = "DELETE FROM CourseAssignStudent";
            Command = new SqlCommand(Query, Connection);
            
            Connection.Open();
            
            RowAffected = Command.ExecuteNonQuery();
            
            Connection.Close();
            
            return RowAffected;
        }
    }
}