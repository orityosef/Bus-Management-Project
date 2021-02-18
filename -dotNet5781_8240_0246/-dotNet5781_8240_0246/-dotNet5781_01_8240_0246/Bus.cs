﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8240_0246
{
    public class Bus
    {
        static public int GlobalKM { get; private set; }
        private const int FULLTANK = 1200;

        public readonly DateTime StartingDate;
        private string license;
        private int km;

        public DateTime Checkup { get; private set; }
        public int Fuel { get; set; }
        public int Km
        {
            get { return km; }
            set
            {
                if (value >= 0)
                {
                    km = value;
                }
            }
        }
        public string License
        {
            get
            {
                return license;
            }

            private set
            {
                if ((StartingDate.Year < 2018 && value.Length == 7) || (StartingDate.Year >= 2018 && value.Length == 8))
                {
                    license = value;
                }
                else
                {
                    throw new Exception("license not valid");
                }
            }
        }

        public Bus()
        {
            Console.WriteLine("give Starting date");
            bool success = DateTime.TryParse(Console.ReadLine(), out StartingDate);
            if (success == false)
            {
                throw new Exception("invalid DateTime string format");
            }
            Console.WriteLine("give license number");
            License = Console.ReadLine();
        }

        public override string ToString()
        {
            string firstpart, middlepart, endpart;
            string formattedLicense;
            if (license.Length == 7)
            {
                // xx-xxx-xx
                firstpart = license.Substring(0, 2);
                middlepart = license.Substring(2, 3);
                endpart = license.Substring(5, 2);
                formattedLicense = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
            }
            else
            {
                // xxx-xx-xxx
                firstpart = license.Substring(0, 3);
                middlepart = license.Substring(3, 2);
                endpart = license.Substring(5, 3);
                formattedLicense = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
            }
            return String.Format("license is: {0,-10}, starting date: {1}", formattedLicense, StartingDate);
        }

        public DateTime Maintenance()
        {
            Checkup = DateTime.Today;
            return Checkup;
        }
        public DateTime Maintenance(DateTime checkup)
        {
            Checkup = checkup;
            return Checkup;
        }

        public void Refuelling(int fuel)
        {
            Fuel = FULLTANK;
        }


    }
}

