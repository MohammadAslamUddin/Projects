using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Projects.BLL;
using Projects.Gateway;
using Projects.Models;
using Rotativa;
using UniversityBITM.BLL;
using UniversityBITM.Models;

namespace Projects.Controllers
{
    public class HomeController : Controller
    {
        private DepartmentManager departmentManager;
        private CourseManager courseManager;
        private TeacherManager teacherManager;
        private CourseAssignToTeacherManager courseAssignToTeacherManager;
        private CourseStatisticsManager courseStatisticsManager;
        private RegisterStudentManager registerStudentManager;
        private AllocateClassRoomManager allocateClassRoomManager;
        private ViewScheduleManger viewScheduleManger;
        private EnrollCourseManager enrollCourseManager;
        private SaveResultManager saveResultManager;
        private ViewResultManager viewResultManager;
        private Teacher teacher;
        private Course course;

        public HomeController()
        {
            departmentManager = new DepartmentManager();
            courseManager = new CourseManager();
            teacherManager = new TeacherManager();
            courseAssignToTeacherManager = new CourseAssignToTeacherManager();
            courseStatisticsManager = new CourseStatisticsManager();
            registerStudentManager = new RegisterStudentManager();
            allocateClassRoomManager = new AllocateClassRoomManager();
            viewScheduleManger = new ViewScheduleManger();
            enrollCourseManager = new EnrollCourseManager();
            saveResultManager = new SaveResultManager();
            viewResultManager = new ViewResultManager();
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDepartment(Department department)
        {
            ViewBag.Message = departmentManager.Save(department);
            return View();
        }

        public ActionResult GetAllDepartments()
        {
            ViewBag.Departments = departmentManager.GetAllDepartments();
            return View();
        }

        public ActionResult SaveCourse()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            ViewBag.Semesters = courseManager.GetAllSemesters();
            return View();
        }

        [HttpPost]
        public ActionResult SaveCourse(Course course)
        {
            ViewBag.Message = courseManager.Save(course);
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            ViewBag.Semesters = courseManager.GetAllSemesters();
            return View();
        }

        public ActionResult SaveTeacher()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            ViewBag.Designations = teacherManager.GetAllDesignations();
            return View();
        }
        [HttpPost]
        public ActionResult SaveTeacher(Teacher teacher)
        {
            ViewBag.Message = teacherManager.Save(teacher);
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            ViewBag.Designations = teacherManager.GetAllDesignations();
            return View();
        }
        
        public ActionResult CourseAssignToTeacher()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            
            return View();
        }

        [HttpPost]
        public JsonResult GetTeacherbyDepartmentId(int id)
        {
            List<Teacher> teachers = courseAssignToTeacherManager.GetTeacherbyDepartmentId(id);
            return Json(teachers);
        }
        [HttpPost]
        public JsonResult GetCoursebyDepartmentId(int id)
        {
            List<Course> courses = courseAssignToTeacherManager.GetCoursebyDepartmentId(id);
            return Json(courses);
        }
        [HttpPost] 
        public JsonResult GetCreditByTeacherId(int id)
        {
            teacher = courseAssignToTeacherManager.GetCreditByTeacherId(id);
            return Json(teacher);
        }

        [HttpPost]
        public JsonResult GetCourseNameByCourseId(int id)
        {
            course = courseAssignToTeacherManager.GetCourseNameByCourseId(id);
            return Json(course);
        }
        [HttpPost]
        public ActionResult CourseAssignToTeacher(CourseAssignToTeacher courseAssignToTeacher)
        {
            ViewBag.Message = courseAssignToTeacherManager.Save(courseAssignToTeacher);
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();

            return View();
        }

        public ActionResult ViewCourseStatistics()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();

            return View();
        }
        [HttpPost]
        public JsonResult GetCourseInfoByDepartmentId (int departmentId)
        {
            List<CourseStatistics> courseStatisticses = courseStatisticsManager.GetAllCourses(departmentId);
            return Json(courseStatisticses);
        }

        public ActionResult RegisterStudent()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            return View();
        }
        [HttpPost]
        public ActionResult RegisterStudent(RegisterStudent student)
        {
            ViewBag.Message = registerStudentManager.Save(student);
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            return View();
        }

        public ActionResult AllocateClassRoom()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            ViewBag.RoomList = allocateClassRoomManager.Rooms();
            ViewBag.DayList = allocateClassRoomManager.Day();
            return View();
        }

        [HttpPost]
        public JsonResult GetCoursesByDepartmentId(int id)
        {
            List<Course> courses = allocateClassRoomManager.GetCoursesByDepartmentId(id);
            return Json(courses);
        }
        [HttpPost]
        public ActionResult AllocateClassRoom(AllocateClassRoom allocateClassRoom)
        {
            ViewBag.Message = allocateClassRoomManager.Save(allocateClassRoom);
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists(); 
            ViewBag.RoomList = allocateClassRoomManager.Rooms();
            ViewBag.DayList = allocateClassRoomManager.Day();
            
            return View();
        }

        public ActionResult ViewSchedule()
        {
            ViewBag.DepartmentLists = departmentManager.GetAllDepartmentsInLists();
            return View();
        }

        [HttpPost]
        public JsonResult GetClassScheduleByDepartmentId(int id)
        {
            List<ViewSchedule> scheduls = viewScheduleManger.GetClassSchedules(id);
            return Json(scheduls);
        }

        public ActionResult EnrollCourse()
        {
            ViewBag.Students = enrollCourseManager.GetAllStudents();
            return View();
        }
        [HttpPost]
        public ActionResult EnrollCourse(EnrollCourse enroll)
        {
            ViewBag.Students = enrollCourseManager.GetAllStudents();
            ViewBag.Message = enrollCourseManager.Save(enroll);
            return View();
        }
        [HttpPost]
        public JsonResult GetStudnetInfoByCourseId(int id)
        {
            RegisterStudent student= enrollCourseManager.GetStudnetInfoByCourseId(id);
            return Json(student);
        }

        [HttpPost]
        public JsonResult GetCoursesByDepId(int id)
        {
            RegisterStudent student = enrollCourseManager.GetDeptIdByStudentId(id);
            List<SelectListItem> courses = enrollCourseManager.GetCoursesByDepId(student.DepartmentId);
            return Json(courses);
        }

        public ActionResult SaveStudentResult()
        {
            ViewBag.Students = enrollCourseManager.GetAllStudents();
            ViewBag.Grades = saveResultManager.GetAllGrades();
            return View();
        }
        [HttpPost]
        public ActionResult SaveStudentResult(SaveResult result)
        {
            ViewBag.Message = saveResultManager.Save(result);
            ViewBag.Students = enrollCourseManager.GetAllStudents(); 
            ViewBag.Grades = saveResultManager.GetAllGrades();
            return View();
        }

        public ActionResult ViewResult()
        {
            ViewBag.Students = enrollCourseManager.GetAllStudents();
            return View();
        }
        [HttpPost]
        public ActionResult ViewResult(SaveResult result)
        {
            ViewBag.Students = enrollCourseManager.GetAllStudents();
            return View();
        }

        [HttpPost]
        public JsonResult GetResultByStudentId(int id)
        {
            List<Course> courses = viewResultManager.GetResultByStudentId(id);
            return Json(courses);
        }

        public ActionResult MakePDF(int studentRegNo)
        {
            return new ActionAsPdf("DownloadPDF", new{SId = studentRegNo}) {FileName = "ResultPDF"};
        }

        public ActionResult DownloadPDF(int SId)
        {
            DownloadModel model = new DownloadModel();
            model.Student = enrollCourseManager.GetStudnetInfoByCourseId(SId);

            List<Course> courses = viewResultManager.GetResultByStudentId(SId);
            model.Courses = courses;
            ViewBag.Data = model;

            return View();
        }

        public ActionResult UnassignCourses()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UnassignCourses(string a)
        {
            ViewBag.Message = courseAssignToTeacherManager.UnallocateTeacher();
            ViewBag.Message2 = enrollCourseManager.UnassignCourseStudent();
            return View();
        }
        public ActionResult UnallocateRooms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UnallocateRooms(string a)
        {
            ViewBag.Message = allocateClassRoomManager.UnallocateRooms();
            return View();
        }

    }
}