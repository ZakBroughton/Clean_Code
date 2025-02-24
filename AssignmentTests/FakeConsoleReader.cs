using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AssignmentTests
{
    public static class FakeConsoleReader
    {
        private static StringReader stringReader;

        public static void SimulateUserInput(params string[] inputLines)
        {
            string input = string.Join(Environment.NewLine, inputLines);
            stringReader = new StringReader(input);
            Console.SetIn(stringReader);
        }

        public static void ClearInput()
        {
            stringReader?.Dispose();
        }
    }
}