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
                return this.GetId().Equals(newStudent.GetId());
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
        public string GetEnrollmentDate()
        {
            return _enrollmentDate;
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
                int StudentId = rdr.GetInt32(0);
                string StudentName = rdr.GetString(1);
                string EnrollmentDate = rdr.GetString(2);
                Student newStudent = new Student(StudentName, EnrollmentDate, StudentId);
                allStudents.Add(newStudent);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allStudents;
        }
    }

}
