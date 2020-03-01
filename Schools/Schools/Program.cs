using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schools
{
    class Program
    {
        public static void Main()
        {
            var nameSpase = "";
            var exit = false;
            Console.WriteLine("\t(1) Ввод данных в базу данных о школе \n\t(2) Вывод данных из баз данных\n\t(3) Выход");
            Console.Write("Выберите раздел приложения (1-3) [3] ");
            var userKey = 0;
            try
            {
                userKey = int.Parse(Console.ReadLine().ToString());
            }
            catch
            {
                Helper.UnRegistredValue();
            }
            switch (userKey)
            {
                case 1:
                    nameSpase = "Schools.AddDataClasses";
                    break;
                case 2:
                    nameSpase = "Schools.ViewDataClasses";
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    Helper.UnRegistredValue();
                    break;
            }

            if (exit) return;

            var classEnumerable = from t in Assembly.GetExecutingAssembly().GetTypes()
                                  where t.IsClass && t.Namespace == nameSpase && t.Name.Contains("Link")
                                  select t;
            var classNum = 1;
            foreach (var className in classEnumerable)
            {
                var name = Helper.InvokeStringMethod(nameSpase, className.Name, "GetNameClass");
                Console.WriteLine($"\t({classNum}) {name}");
                classNum++;
            }
            Console.Write($"Выберите действие (1-{classEnumerable.Count()}) [{classEnumerable.Count()}] ");
            try
            {
                Helper.InvokeStringMethod(nameSpase, classEnumerable.ToList()[int.Parse(Console.ReadLine()) - 1].Name, "Run");
            }
            catch
            {
                Helper.UnRegistredValue();
            }
        }
        
        
    }
}
