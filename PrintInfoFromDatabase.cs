using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab_1_SQL
{
    //Prints information from database depending on user choice in menu
    static internal class PrintInfoFromDatabase
    {
        //Prints all students at the school
        static internal void AllStudents(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = Menus.SortOrderStudents(connection);
            Console.Clear();
            Console.WriteLine("All students at school: ");

            Helpers.PrintFirstNameLastName(sqlQuery, connection);
            Helpers.ReturnToMenu();
        }

        //Prints all classes at the school
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

        //Prints all students in a specific class
        static internal void AllStudentsInClass(string input, SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine($"Students in class {input}: ");
            string sqlQuery = "SELECT FirstName, LastName FROM Students JOIN Classes ON Classes.ClassId = Students.ClassId_FK WHERE ClassName = @ClassName";
            Helpers.PrintFirstNameLastNameDelimiter("@ClassName", input, sqlQuery, connection);
        }

        //Prints all personnel at the school
        static internal void AllPersonnel(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = "SELECT * FROM Personnel";
            Console.WriteLine("All personnel at school: ");
            
            Helpers.PrintFirstNameLastName(sqlQuery, connection);
            Helpers.ReturnToMenu();
        }

        //Prints all categories
        static internal void AllPersonnelCategories(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = "SELECT DISTINCT Category FROM Personnel WHERE Category is not null";
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

        //Prints all personnel in a specific category
        static internal void AllPersonnelInCategory (string input, SqlConnection connection) 
        {
            Console.Clear();
            string sqlQuery = "SELECT FirstName, LastName, Category FROM Personnel WHERE Category = @Category";
            Console.WriteLine($"Personnel in category {input}: ");
            Helpers.PrintFirstNameLastNameDelimiter("@Category", input, sqlQuery, connection);
        }

        //Prints all grades set the latest month with info on student and course
        static internal void GradesSetLatestMonth(SqlConnection connection)
        {
            Console.Clear();

            string sqlQuery = "SELECT FirstName, LastName, CourseName, Grade FROM Enrollments JOIN Students ON Enrollments.StudentId_FK = Students.StudentId JOIN Courses ON Enrollments.CourseId_FK = Courses.CourseId WHERE GradeSetDate >= DATEADD(MONTH, -1, GETDATE())";
            using (SqlCommand gradesSetLatestMonthCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.WriteLine("Grades set latest month:");

                using (SqlDataReader reader = gradesSetLatestMonthCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName")).TrimEnd();
                        string lastName = reader.GetString(reader.GetOrdinal("LastName")).TrimEnd();
                        string courseName = reader.GetString(reader.GetOrdinal("CourseName")).TrimEnd();
                        int grade = reader.GetInt32(reader.GetOrdinal("Grade"));
                        Console.WriteLine($"{firstName} {lastName}s grade on the {courseName} course: {grade}");
                    }
                }
            }
            Helpers.ReturnToMenu();
        }

        //Prints average, minimum and maximum grades for specific course
        static internal void AverageMinMaxGradeInCourse(SqlConnection connection)
        {
            Console.Clear();

            //Change average grade from int to decimal (2 decimals) to more accurately show number
            //Naming the new "columns" for cleaner look
            string sqlQuery = "SELECT CourseName, CAST(AVG(Grade) AS DECIMAL(10,2)) AS 'AverageGrade', MIN(Grade) AS 'MinGrade', MAX(Grade) AS 'MaxGrade' FROM Enrollments JOIN Courses ON Enrollments.CourseId_FK = Courses.CourseId GROUP BY CourseName";
            using (SqlCommand gradesSetLatestMonthCommand = new SqlCommand(sqlQuery, connection))
            {
                Console.WriteLine("Average, minimum and maximum grade students have received per course:");

                using (SqlDataReader reader = gradesSetLatestMonthCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string courseName = reader.GetString(reader.GetOrdinal("CourseName")).TrimEnd();
                        int minGrade = reader.GetInt32(reader.GetOrdinal("MinGrade"));
                        int maxGrade = reader.GetInt32(reader.GetOrdinal("MaxGrade"));
                        decimal avgGrade = reader.GetDecimal(reader.GetOrdinal("AverageGrade"));
                        
                        Console.WriteLine($"\n{courseName}" +
                            $"\nAverage: {avgGrade}" +
                            $"\nMinimum: {minGrade}" +
                            $"\nMaximum: {maxGrade}");
                    }
                }
            }
            Helpers.ReturnToMenu();
        }
    }
}
