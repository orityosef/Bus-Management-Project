using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BO
{
    public class AdjacentStation
    {
        public int Station1ID { get; set; }
        public int Station2ID { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
