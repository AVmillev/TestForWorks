using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schools.AddDataClasses
{
    class AddSchoolLink
    {
        public static string GetNameClass() { return "Добавить школу"; }
        public static void Run()
        {
            try
            {
                SchoolsDBEntities db = new SchoolsDBEntities();

                var schools = db.Set<School>();
                School school = new School();
                Console.Write("Введите номер школы: ");
                school.Number = Console.ReadLine();
                Console.Write("Введите полное наименование: ");
                school.FullName = Console.ReadLine();
                Console.Write("Введите город: ");
                school.City = Console.ReadLine();
                Console.Write("Введите адрес: ");
                school.Adress = Console.ReadLine();
                Console.Write("Введите телефон: ");
                school.Phone = Console.ReadLine();
                schools.Add(school);
                db.SaveChanges();

                Console.WriteLine("Хотите добавить еще школу? (Д/Н)");
                var ques = Console.ReadLine();
                if (ques.ToLower() == "д" || ques.ToLower() == "y")
                    Run();
                else
                    Program.Main();

            }
            catch (Exception ex)
            {
                Helper.ErrorMsg(ex.Message);
            }
        }
    }
    class AddClassLink
    {
        private static SchoolsDBEntities db = new SchoolsDBEntities();
        public static string GetNameClass() { return "Добавить класс"; }
        public static void Run()
        {
            try
            {
                SchoolsDBEntities db = new SchoolsDBEntities();
                if (db.School.ToList().Count == 0) Helper.DontCreateSchools();                

                var classes = db.Set<Class>();
                Class lClass = new Class();
                List<School> schools = db.School.ToList();
                int schoolNum = 1;
                foreach (var school in schools)
                {
                    Console.WriteLine($"\t({schoolNum}) {school.FullName}");
                    schoolNum++;
                }
                Console.Write("Выберите школу: ");
                try
                {
                    lClass.School = schools.ToList()[int.Parse(Console.ReadLine()) - 1];
                }
                catch
                {
                    Helper.UnRegistredValue();
                }
                Console.Write("Введите номер класса (Только цифры): ");
                lClass.Number = int.Parse(Console.ReadLine());
                Console.Write("Введите групп: ");
                lClass.Group = Console.ReadLine();
                classes.Add(lClass);
                db.SaveChanges();               

                Console.Write("Хотите добавить еще класс? (Д/Н): ");
                var ques = Console.ReadLine();
                if (ques.ToLower() == "д" || ques.ToLower() == "y")
                    Run();
                else
                    Program.Main();
            }
            catch (Exception ex)
            {
                Helper.ErrorMsg(ex.Message);
            }
        }
    }
    class AddStudentLink
    {
        public static string GetNameClass() { return "Добавить ученика"; }
        public static void Run()
        {
            try
            {
                SchoolsDBEntities db = new SchoolsDBEntities();
                if (db.School.ToList().Count == 0) Helper.DontCreateSchools();
                if (db.Class.ToList().Count == 0) Helper.DontCreateClasses();

                var studens = db.Set<Student>();
                Student student = new Student();
                List<School> schools = db.School.ToList();
                int schoolNum = 1;
                foreach (var school in schools)
                {
                    Console.WriteLine($"\t({schoolNum}) {school.FullName}");
                    schoolNum++;
                }
                Console.Write("Выберите школу: ");

                try
                {
                    student.School = schools.ToList()[int.Parse(Console.ReadLine()) - 1];
                }
                catch
                {
                    Helper.UnRegistredValue();
                }
                List<Class> classes = db.Class.Where(x => x.SchoolId == student.School.Id).ToList();
                int classNum = 1;
                foreach (var @class in classes)
                {
                    Console.WriteLine($"\t({classNum}) {@class.Number}{@class.Group}");
                    classNum++;
                }
                Console.Write("Выберите класс: ");
                try
                {
                    student.Class = classes.ToList()[int.Parse(Console.ReadLine()) - 1];
                }
                catch
                {
                    Helper.UnRegistredValue();
                }
                Console.Write("Введите имя: ");
                student.FirstName = Console.ReadLine();
                Console.Write("Введите фмилию: ");
                student.LastName = Console.ReadLine();
                Console.Write("Введите отчество: ");
                student.MiddleName = Console.ReadLine();
                
                studens.Add(student);
                db.SaveChanges();

                

                Console.Write("Хотите добавить еще ученика? (Д/Н): ");
                var ques = Console.ReadLine();
                if (ques.ToLower() == "д" || ques.ToLower() == "y")
                    Run();
                else
                    Program.Main();
            }
            catch (Exception ex)
            {
                Helper.ErrorMsg(ex.Message);
            }
        }
    }
}
