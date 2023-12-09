using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    static internal class Menu
    {
        static internal void StartMenu(SqlConnection connection)
        {
            while (true)
            {
                Console.WriteLine("Start menu");
                Console.WriteLine("1. Get all students" +
                    "\n2. Get all students in a class" +
                    "\n3. Add new personell" +
                    "\n4. Get all personell" +
                    "\n5. Get all grades set within the last month" +
                    "\n6. Average grade per course" +
                    "\n7. Add new student");

                Helpers.EnterOptionMenu();

                switch (Helpers.EnterOptionMenu())
                {
                    case "1":
                        PrintInfoFromDatabase.AllStudents(connection);
                        break;
                    case "2":
                        PrintInfoFromDatabase.AllStudentsInClass(connection);
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        break;
                    case "7":
                        break;
                    default:
                        Helpers.InvalidInputMenu();
                        break;
                }
            }
        }

        static internal void GetPersonnelMenu(SqlConnection connection)
        {
            Console.WriteLine("1. Show all personnel" + 
                "2. Show all personnel in category");
            Helpers.EnterOptionMenu();

            switch (Helpers.EnterOptionMenu())
            {
                case "1":
                    PrintInfoFromDatabase.AllPersonnel(connection);
                    break;
                case "2":
                    PrintInfoFromDatabase.AllPersonnelInCategory(connection);
                    break;
                default:
                    Helpers.InvalidInputMenu();
                    break;
            }
        }

        static internal void ReturnToMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to return to menu");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
