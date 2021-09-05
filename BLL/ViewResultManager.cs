using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projects.Gateway;
using UniversityBITM.Models;

namespace Projects.BLL
{
    public class ViewResultManager
    {
        private ViewResultGateway viewResultGateway;

        public ViewResultManager()
        {
            viewResultGateway = new ViewResultGateway();
        }

        public List<Course> GetResultByStudentId(int id)
        {
            return viewResultGateway.GetResultByStudentId(id);
        }
    }
}