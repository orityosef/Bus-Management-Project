using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;

namespace BO
{
    //ישות עזר מציגה פרטים נוספים על קו ועל זמנים
    public class WayForPass
    {
        public int LineNumber { get; set; }//מספר קו
        public TimeSpan TimeOfTrip { get; set; }//כמה זמן לוקחת הדרך הזאת
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
