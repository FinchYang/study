using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var fname = "lalalala.csv";
            var lines = System.IO.File.ReadAllLines(fname);
            var index = 0;
            foreach(var a in lines)
            {
                if (index++ > 8) break;
                var ss = a.Split(',');

                Console.WriteLine(a);
                Console.WriteLine(ss);
            }
            Console.ReadLine();
        }
    }
}
