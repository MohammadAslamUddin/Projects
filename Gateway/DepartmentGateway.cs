using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.Gateway
{
    public class DepartmentGateway : CommonGateway
    {
        public int Save(Department department)
        {
            Query = "INSERT INTO Department VALUES(@name, @code)";
            Command = new SqlCommand(Query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = department.Code;
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = department.Name;
            
            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        public bool IsCodeExist(string code)
        {
            Query = "SELECT * FROM Department WHERE department_code = '" + code + "';";
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

        public bool IsNameExist(string name)
        {
            Query = "SELECT * FROM Department WHERE department_name = '" + name + "';";
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

        public List<Department> GetAllDepartments()
        {
            Query = "SELECT * FROM Department;";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            List<Department> departments = new List<Department>();
            Reader = Command.ExecuteReader();
            while (Reader.Read())
            {
                Department department = new Department();
                department.Id =(int) Reader["department_id"];
                department.Code = Reader["department_code"].ToString();
                department.Name = Reader["department_name"].ToString();

                departments.Add(department);
            }
            Reader.Close();
            Connection.Close();

            return departments;
        }

    }
}