using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;


namespace DLAPI
{

    public interface IDL
    {
        //אוטובוס
        #region Bus
        bool addBus(Bus busNew);
        bool updatingBus(Bus busNew);
        bool deleteBus(Bus busNew);
        IEnumerable<Bus> GetAllBuses();

        Bus GetOneBus(int License);
        #endregion
        //קו
        #region BusLine
        bool addBusLine(Line busLineNew);
        bool updatingBusLine(Line busLineNew);
        bool deleteBusLine(Line busLineNew);
        IEnumerable<Line> GetAllBusesLine();

        Line GetOneBusLine(int Id);
        #endregion BusLine
        //תחנה
        #region Station
        bool addStation(Station StationNew);
        bool updatingStation(Station StationNew);
        bool deleteStation(Station StationNew);
        IEnumerable<Station> GetAllStation();

        Station GetOneStation(int code);
        #endregion Station
        //רשימת תחנות
        #region LineStation
        bool addLineStation(LineStation LineStationNew);
        bool updatingLineStation(LineStation LineStationNew);
        bool deleteLineStation(LineStation SLineStationNew);
        IEnumerable<LineStation> GetAllLineStation();
        IEnumerable<LineStation> getPartOfLineStations(Predicate<LineStation> LineStationDAOCondition);

        LineStation GetOneLineStation(int LineNumber);

        #endregion LineStation
        //משתמשים
        #region User
        bool addUser(User UserNew);
        bool updatingUser(User UserNew);
        bool deleteUser(User UserNew);
        IEnumerable<User> GetAllUser();

        User GetOneUser(string UsserName);
        #endregion User
        //תחנות סמוכות
        #region AdjacentStation
        bool addAdjacentStation(AdjacentStation AdjacentStationNew);
        bool updatingAdjacentStation(AdjacentStation AdjacentStationNew);
        bool deleteAdjacentStation(AdjacentStation AdjacentStationNew);
        IEnumerable<AdjacentStation> GetAllAdjacentStation();
        AdjacentStation GetOneAdjacentStation(int Station1, int Station2);
        #endregion AdjacentStation

    }
}


