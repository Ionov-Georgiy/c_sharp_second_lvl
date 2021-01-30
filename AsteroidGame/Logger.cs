using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AsteroidGame
{
    static class Logger
    {
        internal static void Log(string message)
        {
            Console.WriteLine(message);
            message += "\n";
            using (StreamWriter sw = new StreamWriter("log.txt", true, Encoding.UTF8))
            {
                sw.WriteLine(message);
            }
        }
    }
}
