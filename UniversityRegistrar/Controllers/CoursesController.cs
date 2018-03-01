using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System;

namespace UniversityRegistrar.Controllers
{
    public class CoursesController : Controller
    {

        [HttpGet("/courses")]
        public ActionResult Index()
        {
            List<Course> allCourses= Course.GetAll();
            return View(allCourses);
        }

        [HttpGet("/courses/new")]
        public ActionResult CreateForm()
        {
            return View();
        }
        [HttpPost("/courses")]
        public ActionResult Create()
        {
            Course newCourse = new Course(Request.Form["category-name"], Request.Form["category-number"]);
            newCourse.Save();
            return RedirectToAction("Success", "Home");
        }
    }
}
