using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System;

namespace UniversityRegistrar.Controllers
{
    public class StudentsController : Controller
    {

        [HttpGet("/students")]
        public ActionResult Index()
        {
            List<Student> allStudents = Student.GetAll();
            return View(allStudents);
        }

        [HttpGet("/students/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost("/students")]
        public ActionResult AddStudent()
        {
            Student newStudent = new Student(Request.Form["student-name"], Request.Form["enrollment-date"]);
            newStudent.Save();
            return RedirectToAction("Index");
        }

        [HttpGet("/students/{studentid}/delete")]
        public ActionResult DeleteOne(int studentId)
        {
            Student thisStudent = Student.Find(studentId);
            thisStudent.Delete();
            return RedirectToAction("index");
        }
        [HttpGet("/students/{id}")]
        public ActionResult StudentDetails(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Student selectedStudent = Student.Find(id);
            List<Course> studentCourses = selectedStudent.GetCourses();
            List<Course> allCourses = Course.GetAll();
            model.Add("student", selectedStudent);
            model.Add("studentCourses", studentCourses);
            model.Add("allCourses", allCourses);
            return View( model);

        }

        [HttpPost("/students/{studentId}/courses/new")]
        public ActionResult AddCourse(int studentId)
        {
            Student student = Student.Find(studentId);
            Course course = Course.Find(Int32.Parse(Request.Form["course-id"]));
            student.AddCourse(course);
            return RedirectToAction("Index");
        }

        [HttpGet("/students/{id}/update")]
        public ActionResult UpdateStudentForm(int id)
        {
            Student thisStudent = Student.Find(id);
            return View("updatestudentform", thisStudent);
        }
        [HttpPost("/students/{id}/update")]
        public ActionResult UpdateStudent(int id)
        {
          Student thisStudent = Student.Find(id);
          thisStudent.Update(Request.Form["new-name"], Request.Form["new-enrollment-date"]);
          return RedirectToAction("Index");
        }


    }
}
