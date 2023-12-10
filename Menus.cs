using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    static internal class Menus
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
                        ChooseClass(connection);
                        break;
                    case "3":
                        AddInfoToDatabase.AddNewPersonnel(connection);
                        break;
                    case "4":
                        GetPersonnelMenu(connection);
                        break;
                    case "5":
                        PrintInfoFromDatabase.GradesSetLatestMonth(connection);
                        break;
                    case "6":
                        PrintInfoFromDatabase.AverageGradeInCourse(connection);
                        break;
                    case "7":
                        AddInfoToDatabase.AddNewStudent(connection);
                        break;
                    default:
                        Helpers.InvalidInput();
                        break;
                }
            }
        }

        static internal string SortOrderStudents(SqlConnection connection)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Would you like to sort students by:" +
                    "\n1. First name" +
                    "\n2. Last name");
                Console.Write("Write number option here: ");
                string firstOrLastNameOrder = Console.ReadLine();

                if (firstOrLastNameOrder == "1" || firstOrLastNameOrder == "2")
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Would you like to sort the name in:" +
                            "\n1. Ascending order" +
                            "\n2. Descending order");
                        Console.Write("Write number option here: ");
                        string ascendingOrDescendingOrder = Console.ReadLine();

                        if (ascendingOrDescendingOrder == "1" || ascendingOrDescendingOrder == "2")
                        {

                            if (firstOrLastNameOrder == "1" && ascendingOrDescendingOrder == "1")
                            {
                                return "SELECT FirstName, LastName FROM Students ORDER BY FirstName";
                            }
                            else if (firstOrLastNameOrder == "1" && ascendingOrDescendingOrder == "2")
                            {
                                return "SELECT FirstName, LastName FROM Students ORDER BY FirstName DESC";
                            }
                            else if (firstOrLastNameOrder == "2" && ascendingOrDescendingOrder == "1")
                            {
                                return "SELECT FirstName, LastName FROM Students ORDER BY LastName";
                            }
                            else if (firstOrLastNameOrder == "2" && ascendingOrDescendingOrder == "2")
                            {
                                return "SELECT FirstName, LastName FROM Students ORDER BY LastName DESC";
                            }
                        }
                        else
                        {
                            Helpers.InvalidInput();
                        }
                    }
                }
                else
                {
                    Helpers.InvalidInput();
                }
            }

        }

        static internal void ChooseClass(SqlConnection connection) 
        {
            while (true)
            {
                PrintInfoFromDatabase.AllClasses(connection);
                Console.Write("\nWrite class to show students in that class: ");
                string input = Console.ReadLine().ToUpper();

                if (Helpers.CheckIfClassExist(input, connection))
                {
                    PrintInfoFromDatabase.AllStudentsInClass(input, connection);
                    ReturnToMenu();
                    break;
                }
                else
                {
                    Helpers.InvalidInput();
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
                    ChooseCategory(connection);
                    break;
                default:
                    Helpers.InvalidInput();
                    break;
            }
        }

        static internal void ChooseCategory(SqlConnection connection)
        {
            while (true)
            {
                PrintInfoFromDatabase.AllPersonnelCategories(connection);
                Console.Write("\nEnter category to view personnel: ");
                string input = Console.ReadLine();

                if (Helpers.CheckIfCategoryExist(input, connection))
                {
                    PrintInfoFromDatabase.AllPersonnelInCategory(input, connection);
                    ReturnToMenu();
                    break;
                }
                else
                {
                    Helpers.InvalidInput();
                }
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
