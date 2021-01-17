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
        IEnumerable<Bus> GetPartOfBuses(Predicate<DO.Bus> BusCondition);
        Bus GetOneBus(int License);
        #endregion
        //קו
        #region Line
        bool addLine(Line LineNew);
        bool updatingLine(Line LineNew);
        bool deleteLine(Line LineNew);
        IEnumerable<Line> GetAllBusesLine();
        IEnumerable<Line> GetPartOfBusesLine(Predicate<Line> LineCondition);
        Line GetOneBusLine(string Id);
        #endregion
    }
}
