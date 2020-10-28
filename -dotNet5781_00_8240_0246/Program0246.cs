using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet2020_00_0246
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0246();
            WellcomeYYYY();
            Console.ReadKey();


        }

        private static void WellcomeYYYY()
        {
            Console.WriteLine("Im olso here ");
        }


        private static void Welcome0246()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}
