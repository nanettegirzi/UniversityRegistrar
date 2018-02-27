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
            // Student.DeleteAll();
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
    }
}
