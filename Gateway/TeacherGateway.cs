using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.Gateway
{
    public class TeacherGateway : CommonGateway
    {
        public List<SelectListItem> GetAllDesignations()
        {
            Query = "SELECT * FROM Designation";
            Command = new SqlCommand(Query, Connection);
            
            Connection.Open();
            Reader = Command.ExecuteReader();
            List<SelectListItem> items = new List<SelectListItem>();
            while (Reader.Read())
            {
                SelectListItem item = new SelectListItem();
                item.Value = Reader["designation_id"].ToString();
                item.Text = Reader["designation_name"].ToString();

                items.Add(item);
            }
            Reader.Close();
            Connection.Close();
            return items;
        }

        public int Save(Teacher teacher)
        {
            Query = "INSERT INTO Teacher VALUES(@name, @address, @email, @con , @designation, @dep, @credit, @remainCredit)";
            Command = new SqlCommand(Query, Connection);
            
            Command.Parameters.Clear();
            
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = teacher.Name;
            
            Command.Parameters.Add("address", SqlDbType.VarChar);
            Command.Parameters["address"].Value = teacher.Address;
            
            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = teacher.Email;

            Command.Parameters.Add("con", SqlDbType.VarChar);
            Command.Parameters["con"].Value = teacher.ContactNo;

            Command.Parameters.Add("designation", SqlDbType.Int);
            Command.Parameters["designation"].Value = teacher.Designation;
            
            Command.Parameters.Add("dep", SqlDbType.Int);
            Command.Parameters["dep"].Value = teacher.DepartmentId;
            
            Command.Parameters.Add("credit", SqlDbType.Int);
            Command.Parameters["credit"].Value = teacher.CreditToBeTaken;

            Command.Parameters.Add("remainCredit", SqlDbType.Int);
            Command.Parameters["remainCredit"].Value = teacher.CreditToBeTaken;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        public bool IsEmailExist(string eamil)
        {
            Query = "SELECT * FROM Teacher WHERE teacher_email = '" + eamil + "';";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            bool hasRow = false;
            if (Reader.HasRows)
            {
                hasRow = true;
            }
            Reader.Close();
            Connection.Close();
            return hasRow;
        }
        public List<Teacher> GetTeacherbyDepartmentId(int id)
        {
            Query = "SELECT * FROM Teacher WHERE department_id=@Id";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("Id", SqlDbType.Int);
            Command.Parameters["Id"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<Teacher> teachers = new List<Teacher>();
            while (Reader.Read())
            {
                Teacher teacher = new Teacher();
                teacher.Id = (int)Reader["teacher_id"];
                teacher.Name = Reader["teacher_name"].ToString();
                teacher.Address = Reader["teacher_address"].ToString();
                teacher.ContactNo = Reader["teacher_contact"].ToString();
                teacher.Email = Reader["teacher_email"].ToString();
                teacher.CreditToBeTaken = (int)Reader["teacher_totalcredit"];
                teacher.remainingCredit = (int)Reader["teacher_remaincredit"];

                teachers.Add(teacher);
            }
            Reader.Close();
            Connection.Close();

            return teachers;
        }
        public Teacher GetCreditByTeacherId(int id)
        {
            Query = "SELECT * FROM Teacher WHERE teacher_id=@id";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("id", SqlDbType.Int);
            Command.Parameters["id"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            Teacher teacher = new Teacher();
            while (Reader.Read())
            {
                teacher.CreditToBeTaken = (int)Reader["teacher_totalcredit"];
                teacher.remainingCredit = (int)Reader["teacher_remaincredit"];
            }
            Reader.Close();
            Connection.Close();

            return teacher;
        }
    }
}