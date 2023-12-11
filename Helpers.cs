using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    //Class of methods to help and make it "easier"
    static internal class Helpers
    {
        static internal void ReturnToMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to return to menu");
            Console.ReadLine();
            Console.Clear();
        }

        static internal string EnterOptionMenu()
        {
            Console.Write("\nEnter option number to proceed: ");
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
        
        //Method to print first name and last name with a delimiter,
        //ex. all people in a specific Class och Category
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

        //Checks if user input exist as value in database
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
            string sqlQuery = "SELECT Category FROM Personnel WHERE Category = @Category";
            
            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@Category", input);

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

        static internal int GetClassId(string input, SqlConnection connection)
        {
            string sqlQuery = "SELECT ClassId FROM Classes WHERE ClassName = @ClassName";

            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            {
                command.Parameters.AddWithValue("@ClassName", input);
                return (int)command.ExecuteScalar();
            }
        }
    }
}
