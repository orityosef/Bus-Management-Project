using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil02_Tal_DotNetLab
{
    public class Bus
    {
        public int Mispar { get; set; }
        public DateTime StartYear { get; set; }

        public override string ToString()
        {
            return String.Format("Bus {0} was klita be {1}", Mispar, StartYear.Year.ToString());
        }
    }
}

