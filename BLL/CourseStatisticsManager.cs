using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projects.Gateway;
using Projects.Models;

namespace Projects.BLL
{
    public class CourseStatisticsManager
    {
        CourseStatisticsGateway courseStatisticsGateway = new CourseStatisticsGateway();

        public List<CourseStatistics> GetAllCourses(int depId)
        {
            return courseStatisticsGateway.GetAllCourses(depId);
        }
    }
}