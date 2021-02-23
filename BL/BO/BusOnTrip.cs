using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BO
{
    public class BusOnTrip
    {
        public int IdentifyNumber { get; set; } //מספר מזהה קו
        public TimeSpan TripStart { get; set; }//זמן יציאה
        public override string ToString()
        {
            return this.ToStringProperty();
        }


    }
}
