using System.Data.SqlClient;

namespace Lab_1_SQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the School!");
            Thread.Sleep(2000);
            Console.Clear();

            string connectionString = @"Data Source=(localdb)\.;Initial Catalog=SchoolLab1;Integrated Security=True;Pooling=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                //Opens menu and starts program
                Menus.StartMenu(connection);
            }
        }
    }
}
