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

        static internal void InvalidInputMenu() 
        {
            Console.Clear();
            Console.WriteLine("Invalid input.");
            Thread.Sleep(2000);
            Console.Clear();
        }


        static internal bool CheckIfClassExist(string input, SqlConnection connection)
        {
            string sqlQuery = "SELECT * FROM Classes WHERE ClassName = @ClassName";
            SqlCommand checkClassName = new SqlCommand(sqlQuery, connection);
            checkClassName.Parameters.AddWithValue("@ClassName", input);
            SqlDataReader reader = checkClassName.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                reader.Dispose();
                return true;
            }
            else
            {
                reader.Close();
                reader.Dispose();
                return false;
            }
        }

    }
}
