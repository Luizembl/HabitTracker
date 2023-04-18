using System;
using System.Text;

namespace HabitTracker
{
	internal class Menu
	{
		internal void ShowMenu()
		{
            bool isAppOn = true;

            do
            {
                Console.Clear();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("MAIN MENU");
                sb.AppendLine();
                sb.AppendLine("Please choose one option from the menu Below:");
                sb.AppendLine("Type 0 to quit the app.");
                sb.AppendLine("Type 1 to view all your records.");
                sb.AppendLine("Type 2 to insert a new record.");
                sb.AppendLine("Type 3 to delete a record.");
                sb.AppendLine("Type 4 to update a record.");
                sb.AppendLine("---------------------------------------------");
                sb.AppendLine();
                Console.WriteLine(sb.ToString());

                string selected = Console.ReadLine();

                switch (selected)
                {
                    case "0":
                        Console.WriteLine("Goodbye, see you later!");
                        isAppOn = false;
                        Environment.Exit(0);
                        break;
                    case "1":
                        Logic.GetAllRecords();
                        break;
                    case "2":
                        Logic.InsertRecord();
                        break;
                    case "3":
                        Console.WriteLine("a");
                        break;
                    case "4":
                        Console.WriteLine("b");
                        break;
                    default:
                        Console.WriteLine("Invalid input, please type a number from 0 to 4.");
                        break;
                }

            } while (isAppOn);
        }
	}
}

