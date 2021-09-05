using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projects.Gateway;
using Projects.Models;
using UniversityBITM.Models;

namespace Projects.BLL
{
    public class AllocateClassRoomManager
    {
        private AllocateClassRoomGateway allocateClassRoomGateway;

        public AllocateClassRoomManager()
        {
            allocateClassRoomGateway = new AllocateClassRoomGateway();
        }

        public List<Course> GetCoursesByDepartmentId(int id)
        {
            return allocateClassRoomGateway.GetCoursesByDepartmentId(id);
        }

        public List<SelectListItem> Rooms()
        {
            return allocateClassRoomGateway.Rooms();
        }

        public List<SelectListItem> Day()
        {
            return allocateClassRoomGateway.Day();
        }

        public string Save(AllocateClassRoom allocateClassRoom)
        {
            if (allocateClassRoomGateway.IsClassRoomValid(allocateClassRoom))
            {
                return "The Class Room is not available!";
            }
            else
            {
                int rowAffected = allocateClassRoomGateway.Save(allocateClassRoom);
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

        public string UnallocateRooms()
        {
            int rowAffected = allocateClassRoomGateway.UnallocateRooms();
            if (rowAffected>0)
            {
                return "Unallocated!";
            }
            else
            {
                return "Unallocate Filed!";
            }
        }
    }
}