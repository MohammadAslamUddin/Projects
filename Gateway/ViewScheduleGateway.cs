using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Projects.Models;
using UniversityBITM.Gateway;

namespace Projects.Gateway
{
    public class ViewScheduleGateway : CommonGateway
    {
        public List<ViewSchedule> GetClassSchedules(int id)
        {
            Query = "SELECT * FROM ShowRoomAllocation WHERE department_id = @DepartmentId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = id;

            Connection.Open();

            List<ViewSchedule> viewSchedules = new List<ViewSchedule>();

            Dictionary<string, ViewSchedule> ClassSchedules = new Dictionary<string, ViewSchedule>();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                ViewSchedule view = new ViewSchedule();

                string courseCode = Reader["course_code"].ToString();
                string courseName = Reader["course_name"].ToString();
                string roomNo = Reader["room_no"].ToString();
                string day = Reader["day_shortform"].ToString();
                string fromTime = Reader["from_time"].ToString();
                string toTime = Reader["to_time"].ToString();

                string schedule = "R. No : " + roomNo + " , " + day + " , " + fromTime + " , " + toTime + "<br/>";

                if (roomNo == "")
                {
                    schedule = "Not Assigned Yet";
                }

                if (!ClassSchedules.ContainsKey(courseCode))
                {
                    view.CourseCode = courseCode;
                    view.CourseName = courseName;
                    view.Schedule = schedule;
                    ClassSchedules[courseCode] = view;
                }
                else
                {
                    view = ClassSchedules[courseCode];
                    view.Schedule = view.Schedule + schedule;
                }
                viewSchedules.Add(view);
            }
            Connection.Close();
            return viewSchedules;
        }
    }
}