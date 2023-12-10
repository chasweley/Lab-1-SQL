using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    static internal class Helpers
    {
        static internal string EnterOptionMenu()
        {
            Console.Write("\nEnter menu option number to proceed: ");
            string input = Console.ReadLine();

            return input;
        }

        static internal void InvalidInput() 
        {
            Console.Clear();
            Console.WriteLine("Invalid input.");
            Thread.Sleep(2000);
            Console.Clear();
        }

        static internal void PrintFirstNameLastName(string sqlQuery, SqlConnection connection) 
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
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
        }
        
        static internal void PrintFirstNameLastNameDelimiter(string delimiter, string input, string sqlQuery, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue(delimiter, input);
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
        }

        static internal bool CheckIfClassExist(string input, SqlConnection connection)
        {
            string sqlQuery = "SELECT * FROM Classes WHERE ClassName = @ClassName";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ClassName", input);
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        static internal bool CheckIfCategoryExist(string input, SqlConnection connection)
        {
            string sqlQuery = "SELECT * FROM PersonnelCategories WHERE CategoryName = @CategoryName";
            
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@CategoryName", input);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

    }
}
