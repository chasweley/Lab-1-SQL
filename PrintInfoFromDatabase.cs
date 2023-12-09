using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    static internal class PrintInfoFromDatabase
    {
        //OBS! Användaren får välja om de vill se eleverna sorterade på
        //för- eller efternamn och om det ska vara stigande eller fallande sortering.
        static internal void AllStudents(SqlConnection connection)
        {
            Console.Clear();
            using (SqlCommand command = new SqlCommand("SELECT FirstName, LastName FROM Students", connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName")).TrimEnd();
                        string lastName = reader.GetString(reader.GetOrdinal("LastName")).TrimEnd();

                        Console.WriteLine($"{firstName} {lastName}");
                    }
                }
            }
            Menu.ReturnToMenu();
        }

        static internal void AllClasses(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = "SELECT * FROM Classes";
            Console.WriteLine("Classes: ");
            using (SqlCommand allClassesCommand = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = allClassesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string className = reader.GetString(reader.GetOrdinal("ClassName")).TrimEnd();

                        Console.WriteLine($"{className}");
                    }
                }
            }
        }

        static internal void AllStudentsInClass(SqlConnection connection)
        {
            AllClasses(connection);
            //Fixa felhantering
            string sqlQuery = "SELECT FirstName, LastName FROM Students JOIN Classes ON Classes.ClassId = Students.ClassId_FK WHERE ClassName = @ClassName";
            using (SqlCommand studentsInClassCommand = new SqlCommand(sqlQuery, connection)) 
            {
                Console.Write("\nWrite class to show students in that class: ");
                string input = Console.ReadLine().ToUpper();

                studentsInClassCommand.Parameters.AddWithValue(@"ClassName", input);
                Console.Clear();
                Console.WriteLine($"Students in class {input}: ");
                using (SqlDataReader reader = studentsInClassCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName")).TrimEnd();
                        string lastName = reader.GetString(reader.GetOrdinal("LastName")).TrimEnd();
                        Console.WriteLine($"{firstName} {lastName}");
                    }
                }                
            }
            Menu.ReturnToMenu();
        }

        static internal void AllPersonnel(SqlConnection connection)
        {
            Console.Clear();

            string sqlQuery = "SELECT * FROM Personnel";
            Console.WriteLine("All personnel at school: ");
            using (SqlCommand allPersonnelCommand = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = allPersonnelCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName")).TrimEnd();
                        string lastName = reader.GetString(reader.GetOrdinal("LastName")).TrimEnd();
                        Console.WriteLine($"{firstName} {lastName}");
                    }
                }
            }

            Menu.ReturnToMenu();
        }

        static internal void AllCategories(SqlConnection connection)
        {
            //Ändra så att en category bara visas en gång i listan
            Console.Clear();
            string sqlQuery = "SELECT Category FROM Personnel";
            Console.WriteLine("Categories: ");
            using (SqlCommand allCategoriesCommand = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = allCategoriesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader.GetString(reader.GetOrdinal("Category")).TrimEnd();
                        Console.WriteLine($"{category}");
                    }
                }
            }
        }

        static internal void AllPersonnelInCategory (SqlConnection connection) 
        {
            Console.Clear();
            AllCategories(connection);

            //Fixa felhantering
            string sqlQuery = "SELECT FirstName, LastName FROM Personnel WHERE Category = @Category";
            using (SqlCommand personnelInCategoryCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.Write("\nEnter category to view personnel: ");
                string input = Console.ReadLine();

                personnelInCategoryCommand.Parameters.AddWithValue(@"Category", input);
                Console.Clear();
                Console.WriteLine($"Personnel in category {input}: ");
                using (SqlDataReader reader = personnelInCategoryCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName")).TrimEnd();
                        string lastName = reader.GetString(reader.GetOrdinal("LastName")).TrimEnd();
                        Console.WriteLine($"{firstName} {lastName}");
                    }
                }
            }
            Menu.ReturnToMenu();
        }

        //Metod för att visa betyg som satts senaste månaden
        //(lägg kolumn för datumstämpel på betyg i databasen i Enrollments)
        //Här får användaren se en lista med alla betyg som satts
        //senaste månaden där elevens namn, kursens namn och betyget framgår.


        //Metod för snittbetyg i en kurs
        //Hämta en lista med alla kurser och det snittbetyg som eleverna fått
        //på den kursen samt det högsta och lägsta betyget som någon fått i kursen.
        //Här får användaren se en lista med alla kurser i databasen, snittbetyget
        //samt det högsta och lägsta betyget för varje kurs.
    }
}
