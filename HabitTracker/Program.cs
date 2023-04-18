using System.Data.SQLite;
using System.Text;
using Microsoft.Data.Sqlite;

namespace HabitTrackerApp
{
    class Program
    {
        static string connectionDataBase = "Data Source=HabitTrackerApp.db";

        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionDataBase))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS books_read (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Date TEXT,
                        Quantity INTEGER
                        )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

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

            GetUserinput();
        }

        internal static void GetUserinput()
        {
            Console.Clear();
            bool closeApp = false;

            string selected = Console.ReadLine();

            switch (selected)
            {
                case "0":
                    Console.WriteLine("Goodbye, see you later!");
                    closeApp = true;
                    Environment.Exit(0);
                    break;
                case "1":
                    GetAllRecords();
                    break;
                case "2":
                    InsertRecord();
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
        }

        internal static void GetAllRecords()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionDataBase))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM books_read ";

                SqliteDataReader reader = tableCmd.ExecuteReader();
            }
        }

        internal static void InsertRecord()
        {
            SQLiteConnection connection = new SQLiteConnection("Data Source=HabitTrackerApp.db");
            connection.Open();

            Console.WriteLine("How many books did you read?: ");
            int booksRead = int.Parse(Console.ReadLine());
            Console.WriteLine("What the date? yyyy-mm?: ");
            string date = Console.ReadLine();

            // Insert the habit into the database
            SQLiteCommand insertHabit = new SQLiteCommand("INSERT INTO books_read (books, month) VALUES (@booksRead, @month)", connection);
            insertHabit.Parameters.AddWithValue("@Quantity", booksRead);
            insertHabit.Parameters.AddWithValue("@Date", date);

            insertHabit.ExecuteNonQuery();
            connection.Close();
        }

    }
}
