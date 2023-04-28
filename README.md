# Habit Tracker

Habit Tracker is a console-based application that allows you to keep track of the number of books you read every month.

## Requirements

- .NET Core 3.1
- Microsoft.Data.Sqlite

## Usage

- Clone the repository or download the code as a ZIP file.
- Navigate to the project directory and run the following command in the terminal: dotnet run

## Functionality

- View all your records
- Insert a new record
- Delete a record
- Update a record

## Menu Options

When the application starts, the following menu will be displayed:

```
MAIN MENU

Please choose one option from the menu below:
Type 0 to quit the app.
Type 1 to view all your records.
Type 2 to insert a new record.
Type 3 to delete a record.
Type 4 to update a record.
```

- Type 0 and press Enter to exit the application.
- Type 1 and press Enter to view all your records.
- Type 2 and press Enter to insert a new record.
- Type 3 and press Enter to delete a record.
- Type 4 and press Enter to update a record.

## Note

- The application creates a SQLite database file named `HabitTrackerApp.db` in the project directory when it is run for the first time.
- The database schema contains a single table named `books_read`, which has the following columns:
  - `Id`: integer primary key autoincrement
  - `Month`: integer
  - `Year`: integer
  - `Quantity`: integer
