using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BL.BLAPI
{
    public interface IBL
    {

        //אוטובוס
        #region Bus
        bool addBus(Bus busNew);
        bool updatingBus(Bus busNew);
        bool deleteBus(Bus busNew);
        IEnumerable<Bus> GetAllBuses();
        IEnumerable<Bus> GetPartOfBuses(Predicate<Bus> BusCondition);
        Bus GetOneBus(int License);
        void Treatment(string license);
        void Refuelling(string license);
        #endregion
        //קו
        #region Line
        bool addLine(Line LineNew);
        bool updatingLine(Line LineNew);
        bool deleteLine(Line LineNew);
        IEnumerable<Line> GetAllBusesLine();
        IEnumerable<Line> GetPartOfBusesLine(Predicate<Line> LineCondition);
        Line GetOneBusLine(int Id);
        #endregion
        //תחנה
        #region Station
        bool addStation(Station StationNew);
        bool updatingStation(Station StationNew);
        bool deleteStation(Station StationNew);
        IEnumerable<Station> GetAllStation();
        IEnumerable<Station> GetPartOfStation(Predicate<Station> StationCondition);
        Station GetOneStation(int code);
        #endregion
        //תחנות סמוכות
        #region AdjacentStation
        bool addAdjacentStation(AdjacentStation AdjacentStationNew);
        bool updatingAdjacentStation(AdjacentStation AdjacentStationNew);
        bool deleteAdjacentStation(AdjacentStation AdjacentStationNew);
        IEnumerable<AdjacentStation> GetAllAdjacentStation();
        AdjacentStation GetOneAdjacentStation(int Station1, int Station2);
        AdjacentStation GetOneAdjacentStation2(int Station);
        IEnumerable<AdjacentStation> GetPartOfAdjacentStation(Predicate<AdjacentStation> AdjacentStationCondition);
        #endregion AdjacentStation
        //רשימת תחנות
        #region LineStation
        bool addLineStation(LineStation LineStationNew);
        bool updatingLineStation(LineStation LineStationNew);
        bool deleteLineStation(LineStation SLineStationNew);
        IEnumerable<LineStation> GetAllLineStation();
        IEnumerable<LineStation> GetPartOfLineStation(Predicate<LineStation> LineStationCondition);
        LineStation GetOneLineStation(int LineNumber);

        #endregion LineStation

        #region User
        IEnumerable<User> GetAllUsersBO();//הדפסת כל המשתמשים
        User GetUserBO(string userName);//קבלת פרטי משתמש בודד
        //הוספה, עדכון ומחיקת משתמש
        bool addUser(User user);
        bool updateUser(User user);
        bool deleteUser(User user);

        bool existingUser(string userName, string passWord);

        #endregion
      
        # region  MiniStation
        IEnumerable<MiniStation> GetAllMiniStations();
        IEnumerable<MiniStation> GetListMiniStationsByLine(Line line);//מחזירה את רשימת המיני תחנות של קו ספציפי

        #endregion

        //עבור לוח אלקטרוני
        IEnumerable<LineTimingBO> GetLineTimingsPerStation(Station cuurentStation, TimeSpan now);
        bool addLineTrip(BusOnTrip lineTrip);
        bool deleteLineTrip(BusOnTrip lineTrip);
        List<WayForPass> GetRelevantWays(int codeStation1, int codeStation2);
    }
}
