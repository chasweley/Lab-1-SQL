using System;
using System.Collections.Generic;
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

    }
}
