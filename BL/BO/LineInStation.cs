using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    //קו בתחנה
    //הישות שיש בתוך הרשימה שנמצאת באובייקט תחנה ומכילה את הקויים
    public class LineInStation
    {
        public int IdentifyNumber { get; set; }//מזהה קו
        public int LineNumber { get; set; }//מספר קו
        public string LastStationName { get; set; }//שם תחנה אחרונה
        public int LastStationNum { get; set; }//מספר תחנה אחרונה
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
