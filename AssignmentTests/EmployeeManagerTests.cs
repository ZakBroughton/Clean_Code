using Assignment.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssignmentTests
{
    
    [TestClass]
    public class EmployeeManagerTests
    {
        
        

        [TestMethod]
        public void AddEmployee_WhenEmployeeAdded_ShouldBeFound()
        {
            // Arrange
            var employeeManager = new EmployeeManager();
            var employee = new Employee("John Doe");

            // Act
            employeeManager.AddEmployee(employee);
            var retrievedEmployee = employeeManager.FindEmployee("John Doe");

            // Assert
            Assert.IsNotNull(retrievedEmployee);
            Assert.AreEqual(employee.EmployeeName, retrievedEmployee.EmployeeName);
        }

        [TestMethod]
        public void FindEmployee_WhenEmployeeNotFound_ShouldReturnNull()
        {
            // Arrange
            var employeeManager = new EmployeeManager();
            var employee = new Employee("Jane Smith");

            // Act
            var retrievedEmployee = employeeManager.FindEmployee("Jane Smith");

            // Assert
            Assert.IsNull(retrievedEmployee);
        }

        [TestMethod]
        public void AddEmployee_WhenEmployeeNameAlreadyExists_ShouldThrowException()
        {
            // Arrange
            var employeeManager = new EmployeeManager();
            var employee1 = new Employee("Alice");
            var employee2 = new Employee("Alice");

            // Act
            employeeManager.AddEmployee(employee1);

            // Assert
            Assert.ThrowsException<System.ArgumentException>(() => employeeManager.AddEmployee(employee2));
        }
    }
}