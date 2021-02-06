using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BO
{
    public class Bus
    {
        public int LicenseNum { get; set; }
        public DateTime Fromdate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public int Refuel { get; set; }
        public Status Status { get; set; }
        public override string ToString()
        {
            string firstpart, middlepart, endpart;
            string formattedLicense;
            string license = LicenseNum.ToString();
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
            return String.Format("{0,-10}", formattedLicense);
        }

    }
}
