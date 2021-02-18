using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BO
{
    public class Station
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<int> StationInLineList { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
