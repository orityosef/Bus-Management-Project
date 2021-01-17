using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Line
    {
        public int Id { get; set; }
        public int LineNumber { get; set; }
        public Aeras Aera { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
    }
}
