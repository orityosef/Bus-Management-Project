using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;

namespace BO
{
    //ישות עזר לתחנה מכילה פחות פרטים כדי שיהיה קריא בהצגה של כל תחנה
    public class MiniStation
    {
        public int CodeStation { get; set; }//קוד תחנה
        public string NameStation { get; set; }//שם תחנה
        public override string ToString()
        {
            return CodeStation.ToString() + " / " + NameStation;
        }
    }
}
