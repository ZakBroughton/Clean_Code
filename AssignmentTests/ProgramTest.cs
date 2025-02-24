using Assignment;
using Assignment.ui_command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Assignment.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void GetMenuChoice_ValidInput_ReturnsChoice()
        {
            
            string input = "5\n"; // Simulate user input of '5'
            int expectedChoice = 5;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader(input));

                
                int result = Program.GetMenuChoice();

                
                Assert.AreEqual(expectedChoice, result, "Valid input should return the chosen option");
            }
        }

        [TestMethod]
        public void GetMenuChoice_InvalidInputThenValidInput_ReturnsValidChoice()
        {
           
            string input = "100\n3\n"; // Simulate user input of '100' then '3'
            int expectedChoice = 3;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader(input));

                
                int result = Program.GetMenuChoice();

                
                Assert.AreEqual(expectedChoice, result, "Valid input after invalid input should return the valid choice");
            }
        }

       

        


        [TestMethod]
        public void GetMenuChoice_InvalidInput_LoopUntilValidInput()
        {
            // Arrange
            string input = "abc\n5\n"; // Simulate user input of 'abc' then '5'
            int expectedChoice = 5;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader(input));

                // Act
                int result = Program.GetMenuChoice();

                // Assert
                Assert.AreEqual(expectedChoice, result, "Valid input after invalid input should return the valid choice");
            }
        }

        [TestMethod]
        public void GetMenuChoice_MaximumValue_ReturnsMaximumChoice()
        {
            
            string input = "10\n"; // Simulate user input of '10'
            int expectedChoice = 10;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader(input));

                
                int result = Program.GetMenuChoice();

                
                Assert.AreEqual(expectedChoice, result, "Maximum valid input should return the maximum choice");
            }
        }

        [TestMethod]
        public void GetMenuChoice_MinimumValue_ReturnsMinimumChoice()
        {
            // Arrange
            string input = "1\n"; // Simulate user input of '1'
            int expectedChoice = 1;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader(input));

                
                int result = Program.GetMenuChoice();

                
                Assert.AreEqual(expectedChoice, result, "Minimum valid input should return the minimum choice");
            }
        }

        [TestMethod]
        public void GetMenuChoice_OutOfRangeInputThenValidInput_ReturnsValidChoice()
        {
            
            string input = "-5\n3\n"; // Simulate user input of '-5' then '3'
            int expectedChoice = 3;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                Console.SetIn(new StringReader(input));

                
                int result = Program.GetMenuChoice();

                
                Assert.AreEqual(expectedChoice, result, "Valid input after out-of-range input should return the valid choice");
            }
        }

    }
}