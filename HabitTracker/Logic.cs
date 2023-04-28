using System.Reflection.PortableExecutable;
using System;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Linq;

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

            Logic.GetAllRecords();

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

                reader.Close();

                bool isValid = false;
                int inputId = 0;

                while (isValid == false)
                {
                    Console.WriteLine("Enter the ID of the record to delete:");
                    int userId = int.Parse(Console.ReadLine());

                    tableCmd.CommandText = $"SELECT COUNT(*) FROM books_read WHERE Id = {userId}";
                    int count = Convert.ToInt32(tableCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        inputId = userId;
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid id");
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

                reader.Close();

                bool isValid = false;
                int inputId = 0;

                while (isValid == false)
                {
                    Console.WriteLine("Enter the ID of the record to update:");
                    int userId = int.Parse(Console.ReadLine());

                    tableCmd.CommandText = $"SELECT COUNT(*) FROM books_read WHERE Id = {userId}";
                    int count = Convert.ToInt32(tableCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        inputId = userId;
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Please choose a valid id");
                    }
                }

                Console.WriteLine("Enter the new month:");
                int month = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new year:");
                int year = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the new quantity:");
                int quantity = int.Parse(Console.ReadLine());

                SqliteCommand updateCmd = new SqliteCommand("UPDATE books_read SET Month = @month, Year = @year, Quantity = @quantity WHERE Id = @inputId", connection);
                updateCmd.Parameters.AddWithValue("@month", month);
                updateCmd.Parameters.AddWithValue("@year", year);
                updateCmd.Parameters.AddWithValue("@quantity", quantity);
                updateCmd.Parameters.AddWithValue("@inputId", inputId);

                int rowsAffected = updateCmd.ExecuteNonQuery();

                Console.WriteLine($"Record with ID {inputId} has been updated.");

                connection.Close();
            }
        }
    }
}