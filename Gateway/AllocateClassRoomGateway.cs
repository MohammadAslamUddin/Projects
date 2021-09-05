using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using Projects.Models;
using UniversityBITM.Gateway;
using UniversityBITM.Models;

namespace Projects.Gateway
{
    public class AllocateClassRoomGateway : CommonGateway
    {
        public List<Course> GetCoursesByDepartmentId(int id)
        {
            Query = "SELECT * FROM Course WHERE department_id = @DepartmentId";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = id;

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course course = new Course();
                course.Id =(int) Reader["course_id"];
                course.Code = Reader["course_code"].ToString();
                course.Name = Reader["course_name"].ToString();

                courses.Add(course);
            }
            Reader.Close();
            Connection.Close();

            return courses;
        }

        public List<SelectListItem> Rooms()
        {
            Query = "SELECT * FROM Room";
            Command = new SqlCommand(Query, Connection);

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<SelectListItem> rooms = new List<SelectListItem>();
            while (Reader.Read())
            {
                SelectListItem room = new SelectListItem();
                room.Value = Reader["room_id"].ToString();
                room.Text = Reader["room_no"].ToString();

                rooms.Add(room);
            }
            
            Reader.Close();
            Connection.Close();

            return rooms;
        }

        public List<SelectListItem> Day()
        {
            Query = "SELECT * FROM Day";
            Command = new SqlCommand(Query, Connection);

            Connection.Open();
            Reader = Command.ExecuteReader();
            List<SelectListItem> days = new List<SelectListItem>();
            while (Reader.Read())
            {
                SelectListItem day = new SelectListItem();
                day.Value = Reader["day_id"].ToString();
                day.Text = Reader["day_name"].ToString();

                days.Add(day);
            }

            Reader.Close();
            Connection.Close();

            return days;
        }

        public bool IsClassRoomValid(AllocateClassRoom classRoom)
        {
            int fHour, fMin, tHour, tMin, fH, fM, from, tH, tM, to, checkFrom, checkTo;
            string fTime, tTime, f, t;
            bool check = false;

            int room = classRoom.DayId;

            f = classRoom.FromTime.ToString("HH:mm");
            fH = Convert.ToInt32(f[0].ToString() + f[1].ToString()); 
            fM = Convert.ToInt32(f[3].ToString() + f[4].ToString());
            from = fH * 60 + fM;

            t = classRoom.ToTime.ToString("HH:mm");
            tH = Convert.ToInt32(f[0].ToString() + f[1].ToString()); 
            tM = Convert.ToInt32(f[3].ToString() + f[4].ToString());
            to = tH * 60 + tM;

            Query = "SELECT from_time, to_time FROM ClassRoomAssign WHERE day_id = @dayId AND room_id = @roomId;";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("dayId", SqlDbType.Int);
            Command.Parameters["dayId"].Value = classRoom.DayId;
            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = classRoom.RoomId;

            Connection.Open();

            Reader = Command.ExecuteReader();

            while (Reader.Read())
            {
                fTime = Reader["from_time"].ToString();
                tTime = Reader["to_time"].ToString();

                fHour = Convert.ToInt32(fTime[0].ToString() + fTime[1].ToString());
                fMin = Convert.ToInt32(fTime[3].ToString() + fTime[4].ToString());
                checkFrom = fHour * 60 + fMin;
                    
                tHour = Convert.ToInt32(tTime[0].ToString() + tTime[1].ToString());
                tMin = Convert.ToInt32(tTime[3].ToString() + tTime[4].ToString());
                checkTo = tHour * 60 + tMin;

                if (from < checkFrom && to <= checkFrom)
                {
                    continue;
                }
                else if(from>=checkTo && to>from)
                {
                    continue;
                }
                else
                {
                    check = true;
                    break;
                }
            }
            Reader.Close();
            Connection.Close();
            

            return check;
        }

        public int Save(AllocateClassRoom classRoom)
        {
            string fTime = classRoom.FromTime.ToString("hh:mm");
            string tTime = classRoom.ToTime.ToString("hh:mm");

            Query = "INSERT INTO ClassRoomAssign VALUES(@DepartmentId, @CourseId, @RoomId, @DayId, @fTime, @tTime)";
            Command = new SqlCommand(Query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("DepartmentId", SqlDbType.Int);
            Command.Parameters["DepartmentId"].Value = classRoom.DepartmentId;

            Command.Parameters.Add("CourseId", SqlDbType.Int);
            Command.Parameters["CourseId"].Value = classRoom.CourseId;
            
            Command.Parameters.Add("RoomId", SqlDbType.Int);
            Command.Parameters["RoomId"].Value = classRoom.RoomId;
            
            Command.Parameters.Add("DayId", SqlDbType.Int);
            Command.Parameters["DayId"].Value = classRoom.DayId;

            Command.Parameters.Add("fTime", SqlDbType.Time);
            Command.Parameters["fTime"].Value = fTime;

            Command.Parameters.Add("tTime", SqlDbType.Time);
            Command.Parameters["tTime"].Value = tTime;

            Connection.Open();

            RowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return RowAffected;
        }

        public int UnallocateRooms()
        {
            Query = "DELETE FROM ClassRoomAssign";
            Command = new SqlCommand(Query, Connection);
            Connection.Open();
            int rowAffect = Command.ExecuteNonQuery();
            Connection.Close();
            return rowAffect;
        }
    }
}