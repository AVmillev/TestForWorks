using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schools.ViewDataClasses
{
    class ViewStudentLink
    {
        public static string GetNameClass() { return "Данные о учениках"; }
        public static void Run()
        {
            try
            {
                SchoolsDBEntities db = new SchoolsDBEntities();
                List<Student> students = db.Student.ToList();
                if (students.Count == 0) Helper.ErrorMsg("Данных о учениках нет");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.LastName.Trim()} {student.FirstName.Trim()} {student.MiddleName.Trim()}\t"
                        + $"{student.Class.Number}{student.Class.Group.Trim()}\t{student.School.FullName.Trim()}");
                }
            }
            catch { }
        }
    }
}
