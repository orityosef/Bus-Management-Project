using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
