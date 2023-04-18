﻿using System.Text;
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
                        Date TEXT,
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
                Console.WriteLine("What the date? yyyy-mm?: ");
                string date = Console.ReadLine();

                SqliteCommand insertHabit = new SqliteCommand("INSERT INTO books_read (Quantity, Date) VALUES (@booksQty, @date)", connection);
                insertHabit.Parameters.AddWithValue("@Quantity", booksQty);
                insertHabit.Parameters.AddWithValue("@Date", date);

                insertHabit.ExecuteNonQuery();
                connection.Close();
            }
        }


    }
}

