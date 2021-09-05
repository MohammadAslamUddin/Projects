using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projects.Gateway;
using Projects.Models;

namespace Projects.BLL
{
    public class ViewScheduleManger
    {
        private ViewScheduleGateway viewScheduleGateway;

        public ViewScheduleManger()
        {
            viewScheduleGateway = new ViewScheduleGateway();
        }

        public List<ViewSchedule> GetClassSchedules(int id)
        {
            return viewScheduleGateway.GetClassSchedules(id);
        }
    }
}