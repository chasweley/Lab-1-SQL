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
            string sqlQuery = "INSERT INTO Personnel (FirstName, LastName, Category) VALUES (@FirstName, @LastName, @Category)";

            using (SqlCommand addNewPersonnelCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Category: ");
                string category = Console.ReadLine();

                //Gives variables correct placement in table
                addNewPersonnelCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                addNewPersonnelCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;

                //If category string is not empty add value to database
                if (category != "")
                {
                    addNewPersonnelCommand.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 20).Value = category;
                }
                //If category string is empty send value null to database
                else
                {
                    addNewPersonnelCommand.Parameters.Add("@Category", System.Data.SqlDbType.NVarChar, 20).Value = DBNull.Value;
                }
                addNewPersonnelCommand.ExecuteNonQuery(); //Actually adds to database
                Console.Clear();
                Console.WriteLine($"{firstName} {lastName} with category {category} was added to database.");
            }
            Helpers.ReturnToMenu();
        }

        //Method to add new student to database
        static internal void AddNewStudent(SqlConnection connection)
        {
            Console.Clear();

            string sqlQuery = "INSERT INTO Students (FirstName, LastName, BirthDate, ClassId_FK) VALUES (@FirstName, @LastName, @BirthDate, @ClassId_FK)";
            using (SqlCommand addNewStudentCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Birth date (format YYYY-MM-DD): ");
                DateTime birthDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Class: ");
                string studentClass = Console.ReadLine();

                //Convert DateTime to DateOnly for better looking output when confirming add to database
                DateOnly birthDateOnly = DateOnly.FromDateTime(birthDate);

                //If else statment to make sure class that user wrote actually exists
                //Doesn't add student to database if input = nonexisting class
                if (Helpers.CheckIfClassExist(studentClass, connection) || studentClass == "")
                {
                    addNewStudentCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                    addNewStudentCommand.Parameters.Add("@LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;
                    addNewStudentCommand.Parameters.Add("@BirthDate", System.Data.SqlDbType.Date).Value = birthDate;
                    if (studentClass != "")
                    {
                        addNewStudentCommand.Parameters.Add("@ClassId_FK", System.Data.SqlDbType.Int).Value = Helpers.GetClassId(studentClass, connection);
                    }
                    else
                    {
                        addNewStudentCommand.Parameters.Add("@ClassId_FK", System.Data.SqlDbType.Int).Value = DBNull.Value;
                    }

                    addNewStudentCommand.ExecuteNonQuery();
                    Console.Clear();
                    Console.WriteLine($"Student {firstName} {lastName} with birth date {birthDateOnly} was added to database. Class: {studentClass.ToUpper()}");
                }
                else 
                {
                    Console.WriteLine("\nThat class does not exist. Student was not added to the database.");
                }
            }
            Helpers.ReturnToMenu();
        }
    }
}
