using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    //sealed class DLObject : IDL    //internal

    //{
    //    #region singelton
    //    static readonly DLObject instance = new DLObject();
    //    static DLObject() { }// static ctor to ensure instance init is done just before first usage
    //    DLObject() { } // default => private
    //    public static DLObject Instance { get => instance; }// The public Instance property to use
    //    #endregion

    //    #region Bus
    //    public bool addBus(Bus busNew)
    //    {
    //        if (DataSource.listBus.Exists(busold => busold.LicenseNum == busNew.LicenseNum))
    //        {
    //            throw new BusException(busNew.LicenseNum, "the bus is exists in the system");
    //            //return false;
    //        }

    //        DataSource.listBus.Add(busNew.Clone());
    //        return true;
    //    }

    //    public bool deleteBus(Bus busNew)
    //    {

    //        if (!DataSource.listBus.Exists(busold => busold.LicenseNum == busNew.LicenseNum))
    //        {
    //            throw new BusException(busNew.LicenseNum, "the bus is not exists in the system");
    //            //return false;
    //        }

    //        DataSource.listBus.RemoveAll(busold => busold.LicenseNum == busNew.LicenseNum);
    //        return true;
    //    }

    //    public bool updatingBus(Bus busNew)
    //    {
    //        DO.Bus busold = DataSource.listBus.Find(b => b.LicenseNum == busNew.LicenseNum);
    //        if (busold != null)
    //        {
    //            DataSource.listBus.Remove(busold);
    //            DataSource.listBus.Add(busNew.Clone());
    //            return true;
    //        }
    //        throw new BusException(busNew.LicenseNum, "the bus is exists in the system");
    //        //return false;
    //    }
    //    public Bus GetOneBus(int License)
    //    {

    //        DO.Bus busold = DataSource.listBus.Find(b => b.LicenseNum == License);
    //        if (busold != null)
    //        {
    //            return busold;

    //        }
    //        throw new DO.BusException(License);
    //    }
    //    public IEnumerable<Bus> GetAllBuses()
    //    {
    //        return from Bus in DataSource.listBus
    //               select Bus.Clone();
    //    }


    //    #endregion Bus

    //    #region Line
    //    public bool addBusLine(Line busLineNew)
    //    {
    //        if (DataSource.listLine.Exists(busold => busold.Id == busLineNew.Id))
    //        {
    //            throw new LineException(busLineNew.Id, "the Line is exists in the system");
    //            //return false;
    //        }

    //        DataSource.listLine.Add(busLineNew.Clone());
    //        return true;
    //    }

        public bool updatingBusLine(Line busLineNew)
        {
            DO.Line Lineold = DataSource.listLine.Find(b => b.Id == busLineNew.Id);
            if (Lineold != null)
            {
                DataSource.listLine.Remove(Lineold);
                DataSource.listLine.Add(busLineNew.Clone());
                return true;
            }
            throw new LineException(busLineNew.Id, "the line is not exists in the system");
            //return false;
        }

        public bool deleteBusLine(Line busLineNew)
        {
     
                if (!DataSource.listLine.Exists(busold => busold.LineNumber == busLineNew.LineNumber))
            {
                throw new LineException(busLineNew.Id, "the bus is not exists in the system");
                //return false;
            }
            DataSource.listLine.RemoveAll(busold => busold.LineNumber != busLineNew.LineNumber);
            return true;
        }

        public IEnumerable<Line> GetAllBusesLine()
        {
            return from Line in DataSource.listLine
                   select Line.Clone();
        }


    //    public Line GetOneBusLine(int Id)
    //    {
    //        DO.Line busold = DataSource.listLine.Find(b => b.Id == Id);
    //        if (busold != null)
    //        {
    //            return busold;

    //        }
    //        throw new LineException("the Line is not exists in the system");
    //    }

    //    #endregion Line

    //    #region Station
    //    public bool addStation(Station StationNew)
    //    {
    //        if (DataSource.listStation.Exists(Stationold => Stationold.Code == StationNew.Code))
    //        {
    //            throw new StationException(StationNew.Code, "the station is exists in the system");
    //            //return false;
    //        }

    //        DataSource.listStation.Add(StationNew.Clone());
    //        return true;
    //    }

    //    public bool updatingStation(Station StationNew)
    //    {
    //        DO.Station StationOld = DataSource.listStation.Find(b => b.Code == StationNew.Code);
    //        if (StationOld != null)
    //        {
    //            DataSource.listStation.Remove(StationOld);
    //            DataSource.listStation.Add(StationNew.Clone());
    //            return true;
    //        }
    //        throw new StationException(StationNew.Code, "the Station is not exists in the system");
    //        //return false;
    //    }

    //    public bool deleteStation(Station StationNew)
    //    {
    //        if (!DataSource.listStation.Exists(StationOld => StationOld.Code == StationNew.Code))
    //        {
    //            throw new StationException(StationNew.Code, "the bus is not exists in the system");
    //            //return false;
    //        }
    //        DataSource.listStation.RemoveAll(StationOld => StationOld.Code != StationNew.Code);
    //        return true;
    //    }

    //    public IEnumerable<Station> GetAllStation()
    //    {
    //        return from Station in DataSource.listStation
    //               select Station.Clone();
    //    }



    //    public Station GetOneStation(int code)
    //    {
    //        DO.Station Stationold = DataSource.listStation.Find(b => b.Code == code);
    //        if (Stationold != null)
    //        {
    //            return Stationold;

    //        }
    //        throw new StationException(code, "the Station is not exists in the system");
    //    }
    //    #endregion Station

    //    #region LineStation
    //    public bool addLineStation(LineStation LineStationNew)
    //    {
    //        if (DataSource.listLineStation.Exists(LineStationold => LineStationold.LineNumber == LineStationNew.LineNumber))
    //        {
    //            throw new LineException(LineStationNew.LineNumber, "the Line Station is exists in the system");
    //            //return false;
    //        }

    //        DataSource.listLineStation.Add(LineStationNew.Clone());
    //        return true;
    //    }
    //    public IEnumerable<LineStation> getPartOfLineStations(Predicate<LineStation> LineStationDAOCondition)
    //    {
    //        IEnumerable<LineStation> TempLineStationDAO = from LineStation item in DataSource.listLineStation
    //                                                      where LineStationDAOCondition(item)
    //                                                      select item.Clone();
    //        if (TempLineStationDAO.Count() == 0)
    //            throw new LineStationDException("There are no line stations that meet the condition");
    //        return TempLineStationDAO;
    //    }

    //    public bool updatingLineStation(LineStation LineStationNew)
    //    {
    //        DO.LineStation LineStationold = DataSource.listLineStation.Find(b => b.LineNumber == LineStationNew.LineNumber);
    //        if (LineStationold != null)
    //        {
    //            DataSource.listLineStation.Remove(LineStationold);
    //            DataSource.listLineStation.Add(LineStationNew.Clone());
    //            return true;
    //        }
    //        throw new LineException(LineStationNew.LineNumber, "the Line Station is not exists in the system");
    //        //return false;
    //    }

    //    public bool deleteLineStation(LineStation LineStationNew)
    //    {
    //        if (DataSource.listLineStation.Exists(LineStationold => LineStationold.LineNumber != LineStationNew.LineNumber))
    //        {
    //            throw new LineException(LineStationNew.LineNumber, "the Line Station is not exists in the system");
    //            //return false;
    //        }
    //        DataSource.listLineStation.RemoveAll(LineStationold => LineStationold.LineNumber != LineStationNew.LineNumber);
    //        return true;
    //    }

    //    public IEnumerable<LineStation> GetAllLineStation()
    //    {
    //        return from LineStation in DataSource.listLineStation
    //               select LineStation.Clone();
    //    }


    //    public LineStation GetOneLineStation(int LineNumber)
    //    {
    //        DO.LineStation LineStationold = DataSource.listLineStation.Find(b => b.LineNumber == LineNumber);
    //        if (LineStationold != null)
    //        {
    //            return LineStationold;

    //        }
    //        throw new LineException(LineNumber, "the LineStation is not exists in the system");
    //    }

    //    #endregion LineStation

    //    #region User
    //    public bool addUser(User UserNew)
    //    {
    //        if (DataSource.listUser.Exists(Userold => Userold.UserName == UserNew.UserName))
    //        {
    //            throw new UserException(UserNew.UserName, "the User is exists in the system");
    //            //return false;
    //        }

    //        DataSource.listUser.Add(UserNew.Clone());
    //        return true;
    //    }

    //    public bool updatingUser(User UserNew)
    //    {
    //        DO.User Userold = DataSource.listUser.Find(b => b.UserName == UserNew.UserName);
    //        if (Userold != null)
    //        {
    //            DataSource.listUser.Remove(Userold);
    //            DataSource.listUser.Add(UserNew.Clone());
    //            return true;
    //        }
    //        throw new UserException(UserNew.UserName, "the User is not exists in the system");
    //        //return false;
    //    }

    //    public bool deleteUser(User UserNew)
    //    {
    //        if (DataSource.listUser.Exists(Userold => Userold.UserName != UserNew.UserName))
    //        {
    //            throw new UserException(UserNew.UserName, "the User is not exists in the system");
    //            //return false;
    //        }
    //        DataSource.listUser.RemoveAll(Userold => Userold.UserName != UserNew.UserName);
    //        return true;
    //    }

    //    public IEnumerable<User> GetAllUser()
    //    {
    //        return from User in DataSource.listUser
    //               select User.Clone();
    //    }



    //    public User GetOneUser(string UsserName)
    //    {
    //        DO.User Userold = DataSource.listUser.Find(b => b.UserName.ToString() == UsserName);
    //        if (Userold != null)
    //        {
    //            return Userold;

    //        }
    //        throw new UserException(UsserName, "the User is not exists in the system");
    //    }

    //    #endregion User

    //    #region AdjacentStation
    //    public bool addAdjacentStation(AdjacentStation AdjacentStationNew)
    //    {
    //        if (DataSource.listAdjacentStation.Exists(AdjacentStationold => (AdjacentStationold.Station1 == AdjacentStationNew.Station1) && (AdjacentStationold.Station2 == AdjacentStationNew.Station2)))
    //        {
    //            throw new AdjacentStationException(AdjacentStationNew.Station1, AdjacentStationNew.Station2);
    //            //return false;
    //        }
    //        var cloned = AdjacentStationNew.Clone();
    //        DataSource.listAdjacentStation.Add(cloned);
    //        return true;
    //    }

    //    public bool updatingAdjacentStation(AdjacentStation AdjacentStationNew)
    //    {
    //        DO.AdjacentStation AdjacentStationold = DataSource.listAdjacentStation.Find(A => (A.Station1 == AdjacentStationNew.Station1) && (A.Station2 == AdjacentStationNew.Station2));
    //        if (AdjacentStationold != null)
    //        {
    //            DataSource.listAdjacentStation.Remove(AdjacentStationold);
    //            DataSource.listAdjacentStation.Add(AdjacentStationNew.Clone());
    //            return true;
    //        }
    //        throw new AdjacentStationException(AdjacentStationNew.Station1, AdjacentStationNew.Station2, "the AdjacentStation is not exists in the system");
    //        //return false;
    //    }

    //    public bool deleteAdjacentStation(AdjacentStation AdjacentStationNew)
    //    {
    //        if (DataSource.listAdjacentStation.Exists(AdjacentStationold => (AdjacentStationold.Station1 != AdjacentStationNew.Station1) && (AdjacentStationold.Station2 != AdjacentStationNew.Station2)))
    //        {
    //            throw new AdjacentStationException(AdjacentStationNew.Station1, AdjacentStationNew.Station2, "the AdjacentStation is not exists in the system");
    //            //return false;

    //        }
    //        DataSource.listAdjacentStation.Remove(AdjacentStationNew);
    //        return true;
    //    }

    //    public IEnumerable<AdjacentStation> GetAllAdjacentStation()
    //    {
    //        return from stations in DataSource.listAdjacentStation
    //               select stations.Clone();
    //    }



    //    //public AdjacentStation GetOneAdjacentStation(int Station1, int Station2)
    //    //{
    //    //    DO.AdjacentStation AdjacentStationold = DataSource.listAdjacentStation.Find(b => (b.Station1== Station1)&& (b.Station2 == Station2));
    //    //    if (AdjacentStationold != null)
    //    //    {
    //    //        return AdjacentStationold;

    //    //    }
    //    //    throw new AdjacentStationException(Station1, Station2, "the AdjacentStation is not exists in the system");
    //    //}
    //    public AdjacentStation GetOneAdjacentStation(int Station1, int Station2)
    //    {
    //        DO.AdjacentStation stations1 = DataSource.listAdjacentStation.Find(p => p.Station1 == Station1 && p.Station2 == Station2 || p.Station2 == Station1 && p.Station1 == Station2);

    //        if (stations1 != null)
    //            return stations1.Clone();
    //        else
    //            return null;//throw new DO.PairConsecutiveStationsExceptionDO("No object found for this pair of stations");
    //    }

    //    #endregion AdjacentStation


    //}
}
