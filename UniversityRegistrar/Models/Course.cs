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

        public override bool Equals(System.Object otherCourse)
        {
            if (!(otherCourse is Course))
            {
                return false;
            }
            else
            {
                Course newCourse = (Course) otherCourse;
                bool idEquality = this.GetId() == newCourse.GetId();
                bool courseNameEquality = this.GetCourseName() == newCourse.GetCourseName();
                bool courseNumberEquality = this.GetCourseNumber() == newCourse.GetCourseNumber();
                return (idEquality && courseNameEquality && courseNumberEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetCourseName()
        {
            return _courseName;
        }

        public void SetCourseName(string newCourseName)
        {
            _courseName = newCourseName;
        }
        public string GetCourseNumber()
        {
            return _courseNumber;
        }
        public void SetCourseNumber(string newCourseNumber)
        {
            _courseNumber = newCourseNumber;
        }
        public int GetId()
        {
            return _id;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM courses;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Course> GetAll()
        {
            List<Course> allCourses = new List<Course> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM courses;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int CourseId = rdr.GetInt32(0);
                string CourseName = rdr.GetString(1);
                string CourseNumber = rdr.GetString(2);
                Course newCourse = new Course(CourseName, CourseNumber, CourseId);
                allCourses.Add(newCourse);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCourses;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO courses (course_name, course_number) VALUES (@CourseName, @CourseNumber);";

            MySqlParameter courseName = new MySqlParameter();
            courseName.ParameterName = "@CourseName";
            courseName.Value = this._courseName;
            cmd.Parameters.Add(courseName);

            MySqlParameter courseNumber= new MySqlParameter();
            courseNumber.ParameterName = "@CourseNumber";
            courseNumber.Value = this._courseNumber;
            cmd.Parameters.Add(courseNumber);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Course Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM courses WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int courseId = 0;
            string courseName = "";
            string courseNumber = "";


            while (rdr.Read())
            {
                courseId = rdr.GetInt32(0);
                courseName = rdr.GetString(1);
                courseNumber = rdr.GetString(2);
            }

            Course foundCourse = new Course(courseName, courseNumber, courseId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundCourse;
        }

        public void Update(string newCourseName, string newCourseNumber)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE courses SET course_name = @newCourseName, course_number = @newCourseNumber WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter courseName = new MySqlParameter();
            courseName.ParameterName = "@newCourseName";
            courseName.Value = newCourseName;
            cmd.Parameters.Add(courseName);

            MySqlParameter courseNumber = new MySqlParameter();
            courseNumber.ParameterName = "@newCourseNumber";
            courseNumber.Value = newCourseNumber;
            cmd.Parameters.Add(courseNumber);

            cmd.ExecuteNonQuery();
            _courseName = newCourseName;
            _courseNumber = newCourseNumber;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM courses WHERE id = @CourseId; DELETE FROM students_courses WHERE course_id = @CourseId;";

            MySqlParameter courseIdParameter = new MySqlParameter();
            courseIdParameter.ParameterName = "@CourseId";
            courseIdParameter.Value = this.GetId();
            cmd.Parameters.Add(courseIdParameter);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void AddStudent(Student newStudent)
    {
        MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);";

            MySqlParameter course_id = new MySqlParameter();
            course_id.ParameterName = "@CourseId";
            course_id.Value = _id;
            cmd.Parameters.Add(course_id);

            MySqlParameter student_id = new MySqlParameter();
            student_id.ParameterName = "@StudentId";
            student_id.Value = newStudent.GetId();
            cmd.Parameters.Add(student_id);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
    }

        public List<Student> GetStudents()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT students.* FROM courses
                JOIN students_courses ON (courses.id = students_courses.course_id)
                JOIN students ON (students_courses.student_id = students.id)
                WHERE courses.id = @CourseId;";

            MySqlParameter courseIdParameter = new MySqlParameter();
            courseIdParameter.ParameterName = "@CourseId";
            courseIdParameter.Value = _id;
            cmd.Parameters.Add(courseIdParameter);

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Student> students = new List<Student>{};

            while(rdr.Read())
            {
              int studentId = rdr.GetInt32(0);
              string studentName= rdr.GetString(1);
              string studentEnrollmentDate = rdr.GetString(2);

              Student newStudent = new Student(studentName, studentEnrollmentDate, studentId);
              students.Add(newStudent);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return students;
        }

    }
}
