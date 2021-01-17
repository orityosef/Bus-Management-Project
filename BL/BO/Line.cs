using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BO
{
    public class Line
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public Areas Aera { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
        public List<Station> StationList { get; set; }
    }
}
