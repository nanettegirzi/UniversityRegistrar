using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Tests
{
    [TestClass]
    public class CourseTests : IDisposable
    {
        public CourseTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_test;";
        }
        public void Dispose()
        {
            Student.DeleteAll();
            Course.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForSameCourse_Course()
        {
            //Arrange, Act
            Course firstCourse = new Course("Inrto Computer Science", "CS101");
            Course secondCourse = new Course("Inrto Computer Science", "CS101");

            //Assert
            Assert.AreEqual(firstCourse, secondCourse);
        }

        [TestMethod]
        public void GetCourseName_ReturnsCourseName_String()
        {
            string courseName = "Intro to Math";
            string courseNumber = "MATH101";
            Course newCourse = new Course(courseName, courseNumber);

            string result = newCourse.GetCourseName();

            Assert.AreEqual(courseName, result);
        }

        [TestMethod]
        public void GetCourseNumber_ReturnsCourseNumber_String()
        {
            string courseName = "Advanced Psychology";
            string courseNumber = "PSYCH401";
            Course newCourse = new Course(courseName, courseNumber);

            string result = newCourse.GetCourseNumber();

            Assert.AreEqual(courseNumber, result);
        }

        [TestMethod]
        public void GetAll_CoursesEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Course.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAll_ReturnsCourses_CourseList()
        {
            string courseName01 = "Intro to Computer Science";
            string courseName02 = "Web Basics";
            string courseNumber01 = "CS101";
            string courseNumber02= "WB100";

            Course newCourse1 = new Course(courseName01, courseNumber01);
            Course newCourse2 = new Course(courseName02, courseNumber02);
            newCourse1.Save();
            newCourse2.Save();
            List<Course> newCourse = new List<Course> { newCourse1, newCourse2 };

            List<Course> result = Course.GetAll();

            CollectionAssert.AreEqual(newCourse, result);
        }

        [TestMethod]
        public void Save_SavesCourseToDatabase_CourseList()
        {
            Course testCourse = new Course("Math Intro", "MATH100");
            testCourse.Save();
            //Act
            List<Course> result = Course.GetAll();
            List<Course> testList = new List<Course>{testCourse};

            CollectionAssert.AreEqual(testList, result);
        }

        public void Find_FindsCourseinDatabase_Course()
        {
            //Arrange
            Course testCourse = new Course("Biology", "BIO101");
            testCourse.Save();

            //Act
            Course foundCourse = Course.Find(testCourse.GetId());

            //Assert
            Assert.AreEqual(testCourse, foundCourse);
        }

        [TestMethod]
        public void Edit_UpdatesCourseInDatabase_String()
        {
            //Arrange
            string firstCourseName = "Creative Writing";
            string firstCourseNumber = "CRW105";
            Course testCourse = new Course(firstCourseName, firstCourseNumber);
            testCourse.Save();
            string secondCourseName = "Advanced Creative Writing";
            string secondCourseNumber = "CRW410";

            //Act
            testCourse.Update(secondCourseName, secondCourseNumber);

            Course result = Course.Find(testCourse.GetId());

            //Assert
            Assert.AreEqual(testCourse, result);
        }

        [TestMethod]
        public void Edit_DeleteCoureInDatabase_Int()
        {
            //Arrange
            Course testCourse = new Course("Biology", "BIO100");
            testCourse.Save();


            //Act
            Course result = Course.Find(testCourse.GetId());
            Assert.AreEqual(testCourse, result);
            testCourse.Delete();


            //Assert
            Assert.AreEqual(0, Course.GetAll().Count);

        }

        [TestMethod]
        public void Test_AddStudent_AddsStudentToCourse()
        {
            //Arrange
            Course testCourse = new Course("English", "ENG105");
            testCourse.Save();

            Student testStudent = new Student("Biology", "BIO104");
            testStudent.Save();

            Student testStudent2 = new Student("Calculus", "CALC101");
            testStudent2.Save();

            //Act
            testCourse.AddStudent(testStudent);
            testCourse.AddStudent(testStudent2);

            List<Student> result = testCourse.GetStudents();
            List<Student> testList = new List<Student>{testStudent, testStudent2};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetStudents_ReturnsAllCourseStudents_StudentList()
        {
            //Arrange
            Course testCourse = new Course("Math","MATH101");
            testCourse.Save();

            Student testStudent1 = new Student("Joe Smith", "Jan 9");
            testStudent1.Save();

            Student testStudent2 = new Student("Jane Doe", "Oct 15");
            testStudent2.Save();

            //Act
            testCourse.AddStudent(testStudent1);
            List<Student> savedStudents = testCourse.GetStudents();
            List<Student> testList = new List<Student> {testStudent1};

            //Assert
            CollectionAssert.AreEqual(testList, savedStudents);
        }


    }
}
