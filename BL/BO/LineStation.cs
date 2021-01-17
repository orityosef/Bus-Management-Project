using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BO
{
    public class LineStation:IComparable<LineStation>
    {
        public int LineNumber { get; set; }
        public int StationID { get; set; }
        public int LineStationIndex { get; set; }
        public TimeSpan TimeFromPrevious { get; set; }
        public double DistanceFromPrevious { get; set; }

        public int CompareTo(LineStation other)
        {
            return LineStationIndex.CompareTo(other.LineStationIndex);
        }
    }
}
