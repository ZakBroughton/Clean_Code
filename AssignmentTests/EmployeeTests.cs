using Assignment.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTests
{
    [TestClass]
    public class EmployeeTests
    {
        [TestMethod]
        public void Employee_WhenInitializedWithValidName_ShouldSetEmployeeName()
        {
            // Arrange
            string employeeName = "John Doe";

            // Act
            Employee employee = new Employee(employeeName);

            // Assert
            Assert.AreEqual(employeeName, employee.EmployeeName);
        }

        [TestMethod]
        public void Employee_WhenInitializedWithEmptyName_ShouldSetEmployeeNameToEmptyString()
        {
            // Arrange
            string emptyName = string.Empty;

            // Act
            Employee employee = new Employee(emptyName);

            // Assert
            Assert.AreEqual(emptyName, employee.EmployeeName);
        }

        
    }
}