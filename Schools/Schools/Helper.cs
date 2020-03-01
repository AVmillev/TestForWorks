using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Schools
{
    class Helper
    {
        public static string InvokeStringMethod(
            string namespaceName,
            string typeName,
            string methodName)
        {
            Type calledType = Type.GetType(namespaceName + "." + typeName);

            var res = calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public |
                BindingFlags.Static,
                null,
                null,
                new object[] { });
            return res == null ? "" : res.ToString();
        }
        public static void UnRegistredValue()
        {
            ErrorMsg("Указан не существующий пункт");
        }
        public static void DontCreateSchools()
        {
            ErrorMsg("Не созданы школы");
        }
        public static void DontCreateClasses()
        {
            ErrorMsg("Не созданы классы");
        }
        public static void ErrorMsg(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
            Program.Main();
        }
    }
}
