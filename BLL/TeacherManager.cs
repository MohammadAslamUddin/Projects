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
    public class TeacherManager
    {
        TeacherGateway teacherGateway = new TeacherGateway();

        public string Save(Teacher teacher)
        {
            if (teacher.CreditToBeTaken<0)
            {
                return "Credit Can not be a negative value";
            }
            else if (teacherGateway.IsEmailExist(teacher.Email))
            {
                return "Email Must be unique";
            }
            else
            {
                int rowAffected = teacherGateway.Save(teacher);
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

        public List<SelectListItem> GetAllDesignations()
        {
            return teacherGateway.GetAllDesignations();
        }
    }
}