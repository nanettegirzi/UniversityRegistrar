using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Models
{
    public class Course
    {
        private int _id;
        private string _courseName;
        private string _courseNumber;



        public Course (string courseName, string courseNumber, int id =0)
        {
            _id = id;
            _courseName = courseName;
            _courseNumber = courseNumber;
        }

    }
}
