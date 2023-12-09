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
        static internal void AddNewPersonnel(SqlConnection connection)
        {
            Console.Clear();
            //Fixa ev. felhantering och optional på category
            string sqlQuery = "INSERT INTO Personnel VALUES (@FirstName, @LastName, @Category)";
            using (SqlCommand addNewPersonnelCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                Console.Write("Category: ");
                string category = Console.ReadLine();

                addNewPersonnelCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                addNewPersonnelCommand.Parameters.Add(@"LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;
                addNewPersonnelCommand.Parameters.Add(@"Category", System.Data.SqlDbType.NVarChar, 20).Value = category;
                addNewPersonnelCommand.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine($"{firstName} {lastName} with category {category} added to database.");
            }
            Menu.ReturnToMenu();
        }

        static internal void AddNewStudent(SqlConnection connection)
        {
            Console.Clear();
            //Fixa ev. felhantering och hur hantera lägga till klass?
            string sqlQuery = "INSERT INTO Students VALUES (@FirstName, @LastName, @BirthDate)";
            using (SqlCommand addNewStudentCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("First name: ");
                string firstName = Console.ReadLine();
                Console.Write("Last name: ");
                string lastName = Console.ReadLine();
                Console.Write("BirthDate: ");
                string birthDate = Console.ReadLine();

                addNewStudentCommand.Parameters.Add("@FirstName", System.Data.SqlDbType.NVarChar, 50).Value = firstName;
                addNewStudentCommand.Parameters.Add(@"LastName", System.Data.SqlDbType.NVarChar, 50).Value = lastName;
                addNewStudentCommand.Parameters.Add(@"BirthDate", System.Data.SqlDbType.Date).Value = birthDate;
                addNewStudentCommand.ExecuteNonQuery();
                Console.Clear();
                Console.WriteLine($"Student {firstName} {lastName} with birthdate {birthDate} added to database.");
            }
            Menu.ReturnToMenu();
        }
    }
}
