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
    public class RegisterStudentGateway : CommonGateway
    {
        public bool IsEmailUnique(string email)
        {
            Query = "SELECT * FROM Student WHERE student_email = @Email";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("Email", SqlDbType.VarChar);
            Command.Parameters["Email"].Value = email;

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

        public int Save(RegisterStudent student)
        {
            string dep = GetDepartmentCode(student.DepartmentId);
            string code = GetRecentSerial(student.DepartmentId);
            string year = DateTime.Now.Year.ToString();
            DateTime date = DateTime.Today;
            string reg = dep + "-" + year + "-" + code;
            Query = "INSERT INTO Student Values(@Reg, @Name, @Email, @Contact, @Date, @Address, @DepartmentId)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("Reg", SqlDbType.VarChar);
            Command.Parameters["Reg"].Value = reg;

            Command.Parameters.Add("Name",SqlDbType.VarChar);
            Command.Parameters["Name"].Value = student.Name;

            Command.Parameters.Add("Email", SqlDbType.VarChar);
            Command.Parameters["Email"].Value = student.Email;

            Command.Parameters.Add("Contact", SqlDbType.VarChar);
            Command.Parameters["Contact"].Value = student.ContactNo;
            
            Command.Parameters.Add("Date", SqlDbType.VarChar);
            Command.Parameters["Date"].Value = date;

            Command.Parameters.Add("Address", SqlDbType.VarChar);
            Command.Parameters["Address"].Value = student.Address;

            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = student.DepartmentId;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        private string GetRecentSerial(int departmentId)
        {
            Query = "SELECT TOP 1 student_regno FROM student WHERE department_id = @DepartmentId ORDER BY student_id DESC";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = departmentId;

            Connection.Open();

            Reader = Command.ExecuteReader();

            string data;
            if (Reader.Read())
            {
                data = Reader["student_regno"].ToString();
                string s = data.Substring(data.Length - 1);
                int serial = Convert.ToInt32(s) + 1;
                data = serial.ToString("D3");
            }
            else
            {
                int serial = 1;
                data = serial.ToString("D3");
            }
            Reader.Close();
            Connection.Close();

            return data;
        }

        private string GetDepartmentCode(int i)
        {
            Query = "SELECT * FROM Department WHERE department_id = @DepartmentId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = i;

            Connection.Open();
            Reader = Command.ExecuteReader();
            string code = "";
            if (Reader.Read())
            {
                code = Reader["department_code"].ToString();
            }
            Reader.Close();
            Connection.Close();
            return code;
        }
    }
}