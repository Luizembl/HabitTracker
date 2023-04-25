using System.Reflection.PortableExecutable;
using System;
using System.Text;
using Microsoft.Data.Sqlite;

namespace HabitTracker
{
    internal class Logic
    {
        static string connectionDataBase = "Data Source=HabitTrackerApp.db";

        internal void CreateDB()
        {
            using (var connection = new SqliteConnection(connectionDataBase))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS books_read (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Month INTEGER,
                        Year INTEGER,
                        Quantity INTEGER
                        )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
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

                Console.WriteLine("The quantity of books read by month: ");

                while (reader.Read())
                {
                    Console.WriteLine($"Id: {reader["Id"]}, Month: {reader["Month"]}, Year: {reader["Year"]}, Quantity: {reader["Quantity"]}");
                }
                reader.Close();
                connection.Close();
            }
        }

        internal static void InsertRecord()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionDataBase))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                Console.WriteLine("How many books did you read?: ");
                int booksQty = int.Parse(Console.ReadLine());
                Console.WriteLine("Which month did you read? format MM(number) ex: 01 for January or 03 for March: ");
                int monthRead = int.Parse(Console.ReadLine());
                Console.WriteLine("Which year did you read? format YYYY(number) ex: 2022: ");
                int yearRead = int.Parse(Console.ReadLine());

                SqliteCommand insertHabit = new SqliteCommand("INSERT INTO books_read (Quantity, Month, Year) VALUES (@booksQty, @monthRead, @yearRead)", connection);
                insertHabit.Parameters.AddWithValue("@booksQty", booksQty);
                insertHabit.Parameters.AddWithValue("@monthRead", monthRead);
                insertHabit.Parameters.AddWithValue("@yearRead", yearRead);

                insertHabit.ExecuteNonQuery();
                connection.Close();
            }
        }

        internal static void DeleteRecord()
        {
            Console.Clear();

            using (var connection = new SqliteConnection(connectionDataBase))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                List<Habit> habits = new List<Habit>();
                while (reader.Read())
                {
                    var habit = new Habit(Convert.ToInt32(reader["Id"]), Convert.ToInt32(reader["Month"]), Convert.ToInt32(reader["Year"]), Convert.ToInt32(reader["Quantity"]));
                    habits.Add(habit);
                    Console.WriteLine($"Id: {habit.Id}, Month: {habit.Month}, Year: {habit.Year}, Quantity: {habit.Quantity}");
                }

                Console.WriteLine("Enter the ID of the record to delete:");
                int inputId = int.Parse(Console.ReadLine());
                bool isValid = false;

                foreach (Habit hab in habits)
                {
                    if (hab.Id == inputId)
                    {
                        isValid = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Please choose from a valid id");
                        inputId = int.Parse(Console.ReadLine());
                    }
                }
                

                SqliteCommand deleteCmd = new SqliteCommand("DELETE FROM books_read WHERE Id = @inputId", connection);
                deleteCmd.Parameters.AddWithValue("@inputId", inputId);
                int rowsAffected = deleteCmd.ExecuteNonQuery();

                Console.WriteLine($"Record with ID {inputId} has been deleted.");

                connection.Close();
            }
        }

        internal static void UpdateRecord()
        {
            Console.Clear();
            Logic.GetAllRecords();

            Console.WriteLine("Enter the ID of the record to update: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("If you want to update the quantity choose: 1, if you want to update the date choose: 2");
            int newData = int.Parse(Console.ReadLine());

            if (newData == 1)
            {
                Console.WriteLine("Please choose your new quantity");
                int newQty = int.Parse(Console.ReadLine());

                using (var connection = new SqliteConnection(connectionDataBase))
                {
                    connection.Open();
                    var updateCmd = connection.CreateCommand();
                    updateCmd.CommandText = $"UPDATE books_read SET Quantity = @newQty WHERE Id = @id";
                    updateCmd.Parameters.AddWithValue("@newQty", newQty);
                    updateCmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                    connection.Close();
                }
            }
            else if (newData == 2)
            {
                Console.WriteLine("Please choose your new date yyyy-mm");
                string newDate = Console.ReadLine();

                using (var connection = new SqliteConnection(connectionDataBase))
                {
                    connection.Open();
                    var updateCmd = connection.CreateCommand();
                    updateCmd.CommandText = $"UPDATE books_read SET Date = @newDate WHERE Id = @id";
                    updateCmd.Parameters.AddWithValue("@newDate", newDate);
                    updateCmd.Parameters.AddWithValue("@id", id);
                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    Console.WriteLine($"Rows affected: {rowsAffected}");
                    connection.Close();
                }
            }
            else
            {
                Console.WriteLine("Please choose options between 1 and 2");
                Environment.Exit(0);
            }

        }
    }
}