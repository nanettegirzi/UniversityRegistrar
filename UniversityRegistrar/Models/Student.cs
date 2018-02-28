using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace UniversityRegistrar.Models
{
    public class Student
    {
        private int _id;
        private string _name;
        private string _enrollmentDate;



        public Student(string name, string enrollmentDate, int id =0)
        {
            _id = id;
            _name = name;
            _enrollmentDate = enrollmentDate;
        }

        public override bool Equals(System.Object otherStudent)
        {
            if (!(otherStudent is Student))
            {
                return false;
            }
            else
            {
                Student newStudent = (Student) otherStudent;
                bool idEquality = this.GetId() == newStudent.GetId();
                bool nameEquality = this.GetName() == newStudent.GetName();
                bool enrollmentDateEquality = this.GetEnrollmentDate() == newStudent.GetEnrollmentDate();
                return (idEquality && nameEquality && enrollmentDateEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.GetId().GetHashCode();
        }
        public string GetName()
        {
            return _name;
        }

        public void SetName(string newName)
        {
            _name = newName;
        }
        public string GetEnrollmentDate()
        {
            return _enrollmentDate;
        }
        public void SetEnrollmentDate(string newEnrollmentDate)
        {
            _enrollmentDate = newEnrollmentDate;
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
            cmd.CommandText = @"DELETE FROM students;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Student> GetAll()
        {
            List<Student> allStudents = new List<Student> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM students;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int studentId = rdr.GetInt32(0);
                string studentName = rdr.GetString(1);
                string studentEnrollmentDate = rdr.GetString(2);
                Student newStudent = new Student(studentName, studentEnrollmentDate, studentId);
                allStudents.Add(newStudent);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStudents;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO students (name, enrollment_date) VALUES (@StudentName, @StudentEnrollmentDate);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@StudentName";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter enrollmentDate= new MySqlParameter();
            enrollmentDate.ParameterName = "@StudentEnrollmentDate";
            enrollmentDate.Value = this._enrollmentDate;
            cmd.Parameters.Add(enrollmentDate);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Student Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM students WHERE id = @thisId;";

            MySqlParameter thisId = new MySqlParameter();
            thisId.ParameterName = "@thisId";
            thisId.Value = id;
            cmd.Parameters.Add(thisId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;

            int studentId = 0;
            string studentName = "";
            string studentEnrollmentDate = "";


            while (rdr.Read())
            {
                studentId = rdr.GetInt32(0);
                studentName = rdr.GetString(1);
                studentEnrollmentDate = rdr.GetString(2);
            }

            Student foundStudent = new Student(studentName, studentEnrollmentDate, studentId);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }

            return foundStudent;
        }

        public void Edit(string newName, string newEnrollmentDate)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE students SET name = @newName, enrollment_date = @newEnrollmentDate WHERE id = @searchId;";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = _id;
            cmd.Parameters.Add(searchId);

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@newName";
            name.Value = newName;
            cmd.Parameters.Add(name);

            MySqlParameter enrollmentDate = new MySqlParameter();
            enrollmentDate.ParameterName = "@newEnrollmentDate";
            enrollmentDate.Value = newEnrollmentDate;
            cmd.Parameters.Add(enrollmentDate);

            cmd.ExecuteNonQuery();
            _name = newName;
            _enrollmentDate = newEnrollmentDate;

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
            cmd.CommandText = @"DELETE FROM students WHERE id = @StudentId; DELETE FROM students_courses WHERE student_id = @StudentId;";

            MySqlParameter studentIdParameter = new MySqlParameter();
            studentIdParameter.ParameterName = "@StudentId";
            studentIdParameter.Value = this.GetId();
            cmd.Parameters.Add(studentIdParameter);

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        // public void AddCourse(Course newCourse)
        // {
        //     MySqlConnection conn = DB.Connection();
        //     conn.Open();
        //     var cmd = conn.CreateCommand() as MySqlCommand;
        //     cmd.CommandText = @"INSERT INTO student_courses (student_id, course_id) VALUES (@StudentId, @CourseId);";
        //
        //     MySqlParameter course_id = new MySqlParameter();
        //     course_id.ParameterName = "@CourseId";
        //     course_id.Value = newCourse.GetId();
        //     cmd.Parameters.Add(course_id);
        //
        //     MySqlParameter student_id = new MySqlParameter();
        //     student_id.ParameterName = "@StudentId";
        //     student_id.Value = _id;
        //     cmd.Parameters.Add(student_id);
        //
        //     cmd.ExecuteNonQuery();
        //     conn.Close();
        //     if (conn != null)
        //     {
        //         conn.Dispose();
        //     }
        // }

        }
    }
