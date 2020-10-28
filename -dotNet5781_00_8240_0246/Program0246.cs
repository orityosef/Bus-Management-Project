using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _dotNet5781_00_8240_0246
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome0246();
            Welcome8240();
            Console.ReadKey();


        }

        static partial void Welcome8240();

        private static void Welcome0246()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}
