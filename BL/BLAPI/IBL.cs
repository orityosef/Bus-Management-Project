using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        IEnumerable<Line> GetPartOfStation(Predicate<Station> StationCondition);
        Line GetOneStation(int code);
        #endregion
    }
}
