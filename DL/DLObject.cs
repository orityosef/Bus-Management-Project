﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DO;
using DS;

namespace DL
{
    sealed class DLObject : IDL    //internal

    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus
        public bool addBus(Bus busNew)
        {
            if (DataSource.listBus.Exists(busold => busold.LicenseNum == busNew.LicenseNum))
            {
                throw new BusException(busNew.LicenseNum,"the bus is exists in the system");
                //return false;
            }

            DataSource.listBus.Add(busNew.Clone());
            return true;
        }

        public bool deleteBus(Bus busNew)
        {
            if (DataSource.listBus.Exists(busold => busold.LicenseNum != busNew.LicenseNum))
            {
                throw new BusException(busNew.LicenseNum,"the bus is not exists in the system");
                //return false;
            }
            DataSource.listBus.Remove(busNew);
            return true;
        }

        public bool updatingBus(Bus busNew)
        {
            DO.Bus busold = DataSource.listBus.Find(b => b.LicenseNum == busNew.LicenseNum);
            if (busold != null)
            {
                DataSource.listBus.Remove(busold);
                DataSource.listBus.Add(busNew.Clone());
                return true;
            }
            throw new BusException(busNew.LicenseNum, "the bus is exists in the system");
            //return false;
        }
        public Bus GetOneBus(int License)
        {

            DO.Bus busold = DataSource.listBus.Find(b => b.LicenseNum == License);
            if (busold != null)
            {
                return busold;

            }
            throw new DO.BusException(License);
        }
        public IEnumerable<Bus> GetAllBuses()
        {
            return from Bus in DataSource.listBus
                   select Bus.Clone();
        }

        public IEnumerable<Bus> GetPartOfBuses(Predicate<Bus> LineCondition)
        {
            IEnumerable<Bus> TempBus = from Bus item in DataSource.listBus where LineCondition(item) select item.Clone();
            if (TempBus.Count() == 0)
            {
                throw new BusException("There are no conditional buses in the system");
            }
            return TempBus;
        }
        #endregion Bus

        #region Line
        public bool addBusLine(Line busLineNew)
        {
            if (DataSource.listLine.Exists(busold => busold.Id == busLineNew.Id))
            {
                throw new LineException(busLineNew.Id,"the Line is exists in the system");
                //return false;
            }

            DataSource.listLine.Add(busLineNew.Clone());
            return true;
        }

        public bool updatingBusLine(Line busLineNew)
        {
            DO.Line Lineold = DataSource.listLine.Find(b => b.Id == busLineNew.Id);
            if (Lineold != null)
            {
                DataSource.listLine.Remove(Lineold);
                DataSource.listLine.Add(busLineNew.Clone());
                return true;
            }
            throw new LineException(busLineNew.Id,"the line is not exists in the system");
            //return false;
        }

        public bool deleteBusLine(Line busLineNew)
        {

            if (DataSource.listLine.Exists(busold => busold.Id != busLineNew.Id))
            {
                throw new LineException(busLineNew.Id, "the bus is not exists in the system");
                //return false;
            }
            DataSource.listLine.Remove(busLineNew);
            return true;
        }

        public IEnumerable<Line> GetAllBusesLine()
        {
            return from Line in DataSource.listLine
                   select Line.Clone();
        }

        public IEnumerable<Line> GetPartOfBusesLine(Predicate<Line> BusCondition)
        {
            IEnumerable<Line> TempBus = from Line item in DataSource.listLine where BusCondition(item) select item.Clone();
            if (TempBus.Count() == 0)
            {
                throw new LineException("There are no conditional Lines in the system");
            }
            return TempBus;
        }

        public Line GetOneBusLine(string Id)
        {
            DO.Line busold = DataSource.listLine.Find(b => b.Id.ToString() == Id);
            if (busold != null)
            {
                return busold;

            }
            throw new LineException("the Line is not exists in the system");
        }

        #endregion Line

        #region Station
        public bool addStation(Station StationNew)
        {
            if (DataSource.listStation.Exists(Stationold => Stationold.Code == StationNew.Code))
            {
                throw new StationException(StationNew.Code,"the station is exists in the system");
                //return false;
            }

            DataSource.listStation.Add(StationNew.Clone());
            return true;
        }

        public bool updatingStation(Station StationNew)
        {
            DO.Station StationOld = DataSource.listStation.Find(b => b.Code == StationNew.Code);
            if (StationOld != null)
            {
                DataSource.listStation.Remove(StationOld);
                DataSource.listStation.Add(StationNew.Clone());
                return true;
            }
            throw new StationException(StationNew.Code,"the Station is not exists in the system");
            //return false;
        }

        public bool deleteStation(Station StationNew)
        {
            if (DataSource.listStation.Exists(StationOld => StationOld.Code != StationNew.Code))
            {
                throw new StationException(StationNew.Code, "the bus is not exists in the system");
                //return false;
            }
            DataSource.listStation.Remove(StationNew);
            return true;
        }

        public IEnumerable<Station> GetAllStation()
        {
            return from Station in DataSource.listStation
                   select Station.Clone();
        }

        public IEnumerable<Station> GetPartOfStation(Predicate<Station> StationCondition)
        {
            IEnumerable<Station> TempBus = from Station item in DataSource.listStation where StationCondition(item) select item.Clone();
            if (TempBus.Count() == 0)
            {
                throw new StationException( "There are no conditional Station in the system");
            }
            return TempBus;
        }

        public Station GetOneStation(int code)
        {
            DO.Station Stationold = DataSource.listStation.Find(b => b.Code == code);
            if (Stationold != null)
            {
                return Stationold;

            }
            throw new StationException(code, "the Station is not exists in the system");
        }
        #endregion Station

        #region LineStation
        public bool addLineStation(LineStation LineStationNew)
        {
            if (DataSource.listLineStation.Exists(LineStationold => LineStationold.LineNumber == LineStationNew.LineNumber))
            {
                throw new LineException(LineStationNew.LineNumber,"the Line Station is exists in the system");
                //return false;
            }

            DataSource.listLineStation.Add(LineStationNew.Clone());
            return true;
        }

        public bool updatingLineStation(LineStation LineStationNew)
        {
            DO.LineStation LineStationold = DataSource.listLineStation.Find(b => b.LineNumber == LineStationNew.LineNumber);
            if (LineStationold != null)
            {
                DataSource.listLineStation.Remove(LineStationold);
                DataSource.listLineStation.Add(LineStationNew.Clone());
                return true;
            }
            throw new LineException(LineStationNew.LineNumber,"the Line Station is not exists in the system");
            //return false;
        }

        public bool deleteLineStation(LineStation LineStationNew)
        {
            if (DataSource.listLineStation.Exists(LineStationold => LineStationold.LineNumber != LineStationNew.LineNumber))
            {
                throw new LineException(LineStationNew.LineNumber,"the Line Station is not exists in the system");
                //return false;
            }
            DataSource.listLineStation.Remove(LineStationNew);
            return true;
        }

        public IEnumerable<LineStation> GetAllLineStation()
        {
            return from LineStation in DataSource.listLineStation
                   select LineStation.Clone();
        }

        public IEnumerable<LineStation> GetPartOfLineStation(Predicate<LineStation> LineStationCondition)
        {
            IEnumerable<LineStation> TempBus = from LineStation item in DataSource.listLineStation where LineStationCondition(item) select item.Clone();
            if (TempBus.Count() == 0)
            {
                throw new LineException("There are no conditional LineStation in the system");
            }
            return TempBus;
        }

        public LineStation GetOneLineStation(int LineNumber)
        {
            DO.LineStation LineStationold = DataSource.listLineStation.Find(b => b.LineNumber == LineNumber);
            if (LineStationold != null)
            {
                return LineStationold;

            }
            throw new LineException( LineNumber,"the LineStation is not exists in the system");
        }
        #endregion LineStation

        #region User
        public bool addUser(User UserNew)
        {
            if (DataSource.listUser.Exists(Userold => Userold.UsserName == UserNew.UsserName))
            {
                throw new UserException(UserNew.UsserName,"the User is exists in the system");
                //return false;
            }

            DataSource.listUser.Add(UserNew.Clone());
            return true;
        }

        public bool updatingUser(User UserNew)
        {
            DO.User Userold = DataSource.listUser.Find(b => b.UsserName == UserNew.UsserName);
            if (Userold != null)
            {
                DataSource.listUser.Remove(Userold);
                DataSource.listUser.Add(UserNew.Clone());
                return true;
            }
            throw new UserException(UserNew.UsserName, "the User is not exists in the system");
            //return false;
        }

        public bool deleteUser(User UserNew)
        {
            if (DataSource.listUser.Exists(Userold => Userold.UsserName != UserNew.UsserName))
            {
                throw new UserException(UserNew.UsserName, "the User is not exists in the system");
                //return false;
            }
            DataSource.listUser.Remove(UserNew);
            return true;
        }

        public IEnumerable<User> GetAllUser()
        {
            return from User in DataSource.listUser
                   select User.Clone();
        }

        public IEnumerable<User> GetPartOfUser(Predicate<User> UserCondition)
        {
            IEnumerable<User> TempUser = from User item in DataSource.listUser where UserCondition(item) select item.Clone();
            if (TempUser.Count() == 0)
            {
                throw new UserException("There are no conditional User in the system");
            }
            return TempUser;
        }

        public User GetOneUser(string UsserName)
        {
            DO.User Userold = DataSource.listUser.Find(b => b.UsserName.ToString() == UsserName);
            if (Userold != null)
            {
                return Userold;

            }
            throw new UserException(UsserName,"the User is not exists in the system");
        }

        #endregion User

        #region AdjacentStation
        public bool addAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            if (DataSource.listAdjacentStation.Exists(AdjacentStationold => (AdjacentStationold.Station1 == AdjacentStationNew.Station1)&& (AdjacentStationold.Station2 == AdjacentStationNew.Station2)))
            {
                throw new AdjacentStationException(AdjacentStationNew.Station1, AdjacentStationNew.Station2 );
                //return false;
            }

            DataSource.listAdjacentStation.Add(AdjacentStationNew.Clone());
            return true;
        }

        public bool updatingAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            DO.AdjacentStation AdjacentStationold = DataSource.listAdjacentStation.Find(A => (A.Station1 == AdjacentStationNew.Station1) && (A.Station2 == AdjacentStationNew.Station2));
            if (AdjacentStationold != null)
            {
                DataSource.listAdjacentStation.Remove(AdjacentStationold);
                DataSource.listAdjacentStation.Add(AdjacentStationNew.Clone());
                return true;
            }
            throw new AdjacentStationException(AdjacentStationNew.Station1, AdjacentStationNew.Station2, "the AdjacentStation is not exists in the system");
            //return false;
        }

        public bool deleteAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            if (DataSource.listAdjacentStation.Exists(AdjacentStationold => (AdjacentStationold.Station1 != AdjacentStationNew.Station1) && (AdjacentStationold.Station2 != AdjacentStationNew.Station2)))
            {
                throw new AdjacentStationException(AdjacentStationNew.Station1, AdjacentStationNew.Station2, "the AdjacentStation is not exists in the system");
                //return false;
               
            }
            DataSource.listAdjacentStation.Remove(AdjacentStationNew);
            return true;
        }

        public IEnumerable<AdjacentStation> GetAllAdjacentStation()
        {
            return from AdjacentStation in DataSource.listAdjacentStation
                   select AdjacentStation.Clone();
        }

        public IEnumerable<AdjacentStation> GetPartOfAdjacentStation(Predicate<AdjacentStation> AdjacentStationCondition)
        {
            IEnumerable<AdjacentStation> TempAdjacentStation = from AdjacentStation item in DataSource.listAdjacentStation where AdjacentStationCondition(item) select item.Clone();
            if (TempAdjacentStation.Count() == 0)
            {
                throw new AdjacentStationException("There are no conditional AdjacentStation in the system");
            }
            return TempAdjacentStation;
        }

        public AdjacentStation GetOneAdjacentStation(int Station1, int Station2)
        {
            DO.AdjacentStation AdjacentStationold = DataSource.listAdjacentStation.Find(b => (b.Station1== Station1)&& (b.Station2 == Station2));
            if (AdjacentStationold != null)
            {
                return AdjacentStationold;

            }
            throw new AdjacentStationException(Station1, Station2, "the AdjacentStation is not exists in the system");
        }

        #endregion AdjacentStation


    }
}
