using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Assignment.DataAccess;
using Assignment.Models;
using Assignment.ui_command;
using static System.Net.Mime.MediaTypeNames;


namespace Assignment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            try
            {

                CommandFactory factory = new CommandFactory(new DataGatewayFacade());

                await factory.CreateCommand(UI_Command.INITIALISE_DATABASE)
                    .ExecuteAsync();

                UI_Command displayMenu = factory.CreateCommand(UI_Command.DISPLAY_MENU);

                await displayMenu.ExecuteAsync();
                int choice = GetMenuChoice();

                while (choice != UI_Command.EXIT)
                {
                    await factory.CreateCommand(choice)
                        .ExecuteAsync();

                    await displayMenu.ExecuteAsync();
                    choice = GetMenuChoice();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("\nERROR: Test 3: " + e.ToString());
            }
        }

        public static int GetMenuChoice()
        {
            int option = ConsoleReader.ReadInteger("\nOption");
            while (option < 1 || option > 10)
            {
                Console.WriteLine("\nChoice not recognised. Please try again");
                option = ConsoleReader.ReadInteger("\nOption");
            }
            return option;
        }
    }
}