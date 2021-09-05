using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Gateway;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace UniversityBITM.BLL
{
    public class DepartmentManager
    {
        DepartmentGateway departmentGateway = new DepartmentGateway();

        public object Save(Department department)
        {
            if (department.Code.Length<2 || department.Code.Length>7)
            {
                return "Code Must be greater than or equal to 2 and less than or equal to 7";
            }
            else if (department.Name==null || department.Code == null)
            {
                return "Please Type the name and Code!";
            }
            else if (departmentGateway.IsCodeExist(department.Code))
            {
                return "Department Code Should be Unique";
            }
            else if (departmentGateway.IsNameExist(department.Name))
            {
                return "Department Name Should be Unique";
            }
            else
            {
                int rowAffected = departmentGateway.Save(department);
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

        public List<Department> GetAllDepartments()
        {
            return departmentGateway.GetAllDepartments();
        }

        public List<SelectListItem> GetAllDepartmentsInLists()
        {
            List<Department> departments = GetAllDepartments();
            
            
            List<SelectListItem> itemLists = new List<SelectListItem>();
            
            foreach (var dep in departments)
            {
                SelectListItem item = new SelectListItem();
                item.Value = dep.Id.ToString();
                item.Text = dep.Name;

                itemLists.Add(item);
            }

            return itemLists;
        }
    }
}