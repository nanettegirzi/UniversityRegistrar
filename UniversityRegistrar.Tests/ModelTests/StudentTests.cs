using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Tests
{
    [TestClass]
    public class StudentTests : IDisposable
    {
        public StudentTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_test;";
        }
        public void Dispose()
        {
            Student.DeleteAll();
            // Course.DeleteAll();
        }

        [TestMethod]
        public void Equals_OverrideTrueForSameName_Student()
        {
          //Arrange, Act
         Student firstStudent = new Student("Nanette Girzi", "Jan 1");
         Student secondStudent = new Student("Nanette Girzi", "Jan 1");

          //Assert
          Assert.AreEqual(firstStudent, secondStudent);
        }

        [TestMethod]
        public void GetAll_StudentsEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Student.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void GetAll_ReturnsStudents_StudentList()
        {
            string name01 = "Joe Anderson";
            string name02 = "Heather Mills";
            string enrollmentDate01 = "Jan 6";
            string enrollmentDate02 = "Feb 8";

            Student newStudent1 = new Student(name01, enrollmentDate01);
            Student newStudent2 = new Student(name02, enrollmentDate02);
            newStudent1.Save();
            newStudent2.Save();
            List<Student> newStudent = new List<Student> { newStudent1, newStudent2 };

            List<Student> result = Student.GetAll();

            CollectionAssert.AreEqual(newStudent, result);
        }


        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            string studentName = "Sue Smith";
            string enrollmentDate = "Jan 1";
            Student newStudent = new Student(studentName, enrollmentDate);

            string result = newStudent.GetName();

            Assert.AreEqual(studentName, result);
        }

        [TestMethod]
        public void GetEnrollmentDate_ReturnsEnrollmentDate_String()
        {
            string studentName = "Sue Smith";
            string enrollmentDate = "Jan 1";
            Student newStudent = new Student(studentName, enrollmentDate);

            string result = newStudent.GetEnrollmentDate();

            Assert.AreEqual(enrollmentDate, result);
        }

        [TestMethod]
        public void Save_SavesStudentToDatabase_StudentList()
        {
          Student testStudent = new Student("Jen Brown", "Jan 1");
          testStudent.Save();
          //Act
          List<Student> result = Student.GetAll();
          List<Student> testList = new List<Student>{testStudent};

          CollectionAssert.AreEqual(testList, result);
        }



        public void Find_FindsStudentinDatabase_Student()
        {
            //Arrange
          Student testStudent = new Student("Joe Miller", "Jan 5");
          testStudent.Save();

          //Act
          Student foundStudent = Student.Find(testStudent.GetId());

          //Assert
          Assert.AreEqual(testStudent, foundStudent);
        }

        [TestMethod]
        public void Edit_UpdatesStudentInDatabase_String()
        {
            //Arrange
                string firstName = "John Smith";
                string firstEnrollmentDate = "Jan 8";
                Student testStudent = new Student(firstName, firstEnrollmentDate);
                testStudent.Save();
                string secondName = "John Swell";
                string secondEnrollmentDate = "Jan 15";

                //Act
                testStudent.Edit(secondName, secondEnrollmentDate);

                Student result = Student.Find(testStudent.GetId());

                //Assert
                Assert.AreEqual(testStudent, result);
        }

        [TestMethod]
        public void Edit_DeleteStudentInDatabase_Int()
        {
            //Arrange
            Student testStudent = new Student("Heather Girzi", "Jan 13");
            testStudent.Save();


            //Act
            Student result = Student.Find(testStudent.GetId());
            Assert.AreEqual(testStudent, result);
            testStudent.Delete();


            //Assert
            Assert.AreEqual(0, Student.GetAll().Count);

        }

    }
}
