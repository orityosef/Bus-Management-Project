using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;

namespace BO
{
    public class Line
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public Areas Aera { get; set; }
      
        public int FirstStation { get; set; }
        public string FirstStationN { get; set; }
        public int LastStation { get; set; }
        public string LastStationN { get; set; }
        public List<Station> StationList { get; set; }
        public IEnumerable<LineStation> ListOfStations { get; set; }
        public IEnumerable<BusOnTrip> ListOfTrips { get; set; }//רשימת יציאות הקו
        public override string ToString()
        {
            return " ";
           // return this.ToStringProperty();
        }
    }
}
