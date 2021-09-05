using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projects.Gateway;
using Projects.Models;

namespace Projects.BLL
{
    public class RegisterStudentManager
    {
        private RegisterStudentGateway registerStudentGateway;

        public RegisterStudentManager()
        {
            registerStudentGateway = new RegisterStudentGateway();
        }

        public string Save(RegisterStudent student)
        {
            if (registerStudentGateway.IsEmailUnique(student.Email))
            {
                return "Please Enter an unique email";
            }
            else
            {
                int rowAffected = registerStudentGateway.Save(student);
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
    }
}