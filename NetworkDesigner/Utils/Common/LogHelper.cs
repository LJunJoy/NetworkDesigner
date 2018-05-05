using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkDesigner.Utils.Common
{
    class LogHelper
    {
        public static void LogInfo(string info)
        {
            Console.WriteLine(info);
        }
        public static void LogError(Exception exception)
        {
            Console.WriteLine(exception);
        }
        public static void LogError(string info,Exception exception)
        {
            Console.WriteLine(info + exception);
        }
    }
}
