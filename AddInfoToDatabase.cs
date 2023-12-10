using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    internal class AddInfoToDatabase
    {
        //Method to add new personnel to database
        static internal void AddNewPersonnel(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = "INSERT INTO Personnel (FirstName, LastName) VALUES (@FirstName, @LastName)";

            using (SqlCommand addNewPersonnelCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();

                //Gives variables correct placement in table
                addNewPersonnelCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                addNewPersonnelCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;
                addNewPersonnelCommand.ExecuteNonQuery(); //Actually adds to database
                Console.Clear();
                Console.WriteLine($"{firstName} {lastName} was added to database.");
            }
            Helpers.ReturnToMenu();
        }

        //Method to add new student to database
        static internal void AddNewStudent(SqlConnection connection)
        {
            Console.Clear();

            string sqlQuery = "INSERT INTO Students (FirstName, LastName, BirthDate) VALUES (@FirstName, @LastName, @BirthDate)";
            using (SqlCommand addNewStudentCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Birth date (format YYYY-MM-DD): ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());

                //Convert DateTime to DateOnly for better looking output when confirming add to database
                DateOnly birthDateOnly = DateOnly.FromDateTime(birthDate);

                addNewStudentCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                addNewStudentCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;
                addNewStudentCommand.Parameters.Add("@BirthDate", System.Data.SqlDbType.Date).Value = birthDate;
                addNewStudentCommand.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine($"Student {firstName} {lastName} with birth date {birthDateOnly} was added to database.");
            }
            Helpers.ReturnToMenu();
        }
    }
}
