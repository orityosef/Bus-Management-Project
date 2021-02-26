using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BO
{
    public class LineStation:IComparable<LineStation>
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public int StationID { get; set; }
        public int LineStationIndex { get; set; }
        public string NameStation { get; set; }
        public int PrevStation { get; set; }
        public int NextStation { get; set; }
        public TimeSpan TimeFromFirstStation { get; set; }
        public TimeSpan Time { get; set; }
        public double Distance { get; set; }

        public int CompareTo(LineStation other)
        {
            return LineStationIndex.CompareTo(other.LineStationIndex);
        }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
