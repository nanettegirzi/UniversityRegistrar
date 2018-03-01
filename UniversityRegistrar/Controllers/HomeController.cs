using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;
using System;

namespace UniversityRegistrar.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Course> allCourses= Course.GetAll();
            return View(allCourses);
        }

        [HttpGet("/home/success")]
        public ActionResult Success()
        {
            return View();
        }
    }
}
