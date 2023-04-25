using System;
namespace HabitTracker
{
	public class Habit
	{
        public Habit(int id, int quantity, int month, int year)
        {
            Id = id;
            Quantity = quantity;
            Month = month;
            Year = year;
        }

        public int Id { get; set; }

		public int Quantity { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

    }
}

