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
        static internal void AllStudents(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = Menus.SortOrderStudents(connection);
            Console.WriteLine("All students at school: ");

            Helpers.PrintFirstNameLastName(sqlQuery, connection);
            Menus.ReturnToMenu();
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

        static internal void AllStudentsInClass(string input, SqlConnection connection)
        {
            Console.Clear();
            Console.WriteLine($"Students in class {input}: ");
            string sqlQuery = "SELECT FirstName, LastName FROM Students JOIN Classes ON Classes.ClassId = Students.ClassId_FK WHERE ClassName = @ClassName";
            Helpers.PrintFirstNameLastNameDelimiter(@"ClassName", input, sqlQuery, connection);
        }

        static internal void AllPersonnel(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = "SELECT * FROM Personnel";
            Console.WriteLine("All personnel at school: ");
            
            Helpers.PrintFirstNameLastName(sqlQuery, connection);
            Menus.ReturnToMenu();
        }

        static internal void AllPersonnelCategories(SqlConnection connection)
        {
            Console.Clear();
            string sqlQuery = "SELECT * FROM PersonnelCategories";
            Console.WriteLine("Categories: ");
            using (SqlCommand allCategoriesCommand = new SqlCommand(sqlQuery, connection))
            {
                using (SqlDataReader reader = allCategoriesCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string category = reader.GetString(reader.GetOrdinal("CategoryName")).TrimEnd();
                        Console.WriteLine($"{category}");
                    }
                }
            }
        }

        static internal void AllPersonnelInCategory (string input, SqlConnection connection) 
        {
            Console.Clear();
            string sqlQuery = "SELECT FirstName, LastName FROM Personnel JOIN PersonnelCategories ON PersonnelCategories.CategoryId = Personnel.CategoryId_FK WHERE CategoryName = @CategoryName";
            Console.WriteLine($"Personnel in category {input}: ");
            Helpers.PrintFirstNameLastNameDelimiter(@"CategoryName", input, sqlQuery, connection);
        }

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
            Menus.ReturnToMenu();
        }

        static internal void AverageGradeInCourse(SqlConnection connection)
        {
            Console.Clear();

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
                        
                        Console.WriteLine($"{courseName}: Average {avgGrade} - Minimum {minGrade} - Maximum {maxGrade}");
                    }
                }
            }
            Menus.ReturnToMenu();
        }
    }
}
