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
                    "\n3. Add new personnel" +
                    "\n4. Get all personnel" +
                    "\n5. Get all grades set within the last month" +
                    "\n6. Average grade per course" +
                    "\n7. Add new student");

                switch (Helpers.EnterOptionMenu())
                {
                    case "1":
                        PrintInfoFromDatabase.AllStudents(connection);
                        break;
                    case "2":
                        PrintInfoFromDatabase.AllStudentsInClass(connection);
                        break;
                    case "3":
                        AddInfoToDatabase.AddNewPersonnel(connection);
                        break;
                    case "4":
                        GetPersonnelMenu(connection);
                        break;
                    case "5":
                        //Hämta alla betyg som satts den senaste månaden
                        break;
                    case "6":
                        //Snittbetyg per kurs
                        break;
                    case "7":
                        AddInfoToDatabase.AddNewStudent(connection);
                        break;
                    default:
                        Helpers.InvalidInputMenu();
                        break;
                }
            }
        }

        static internal void GetPersonnelMenu(SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine("1. Show all personnel" + 
                "\n2. Show all personnel in category");

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
