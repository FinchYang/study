using System;

namespace usda
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!"+Console.OutputEncoding.ToString());
            var a=936;
            var b=a.ToString("X");
            var c="☯";
            Console.OutputEncoding=System.Text.Encoding.Unicode;
            Console.WriteLine(c+"Hello World!"+b+Console.OutputEncoding.ToString());
        }
    }
}
