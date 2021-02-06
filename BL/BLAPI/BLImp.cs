using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using BLAPI;


namespace BL.BLAPI
{
    class BLImp : IBL //internal
    {
        readonly IDL dl = DLFactory.GetDL();


        #region Bus
        //המרה מ-BלD
        private DO.Bus ConvertBtoD(Bus busBo)
        {
            DO.Bus busDo = new DO.Bus();
            busDo.LicenseNum = busBo.LicenseNum;
            busDo.Fromdate = busBo.Fromdate;
            busDo.TotalTrip = busBo.TotalTrip;
            busDo.FuelRemain = busBo.FuelRemain;
            busDo.Refuel = busBo.Refuel;
            busDo.Status = (DO.Status)busBo.Status;
            return (busDo);
        }
        //המרה מ-DלB
        private Bus ConvertDtoB(DO.Bus busDo)
        {
            Bus busBo = new Bus
            {
                LicenseNum = busDo.LicenseNum,
                Fromdate = busDo.Fromdate,
                TotalTrip = busDo.TotalTrip,
                FuelRemain = busDo.FuelRemain,
                Refuel = busDo.Refuel,
                Status = (Status)busDo.Status
            };

            return (busBo);
        }
        //הוספת אוטובוס 
        public bool addBus(Bus busNew)
        {
            DO.Bus busDo = new DO.Bus();
            busDo = ConvertBtoD(busNew);
            try
            {
                dl.addBus(busDo);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException("bus license is illegal", ex);
                //return false
            }
            return true;
        }
        //מחיקה אוטובוס
        public bool deleteBus(Bus busNew)
        {
            DO.Bus busDo = new DO.Bus();
            busDo = ConvertBtoD(busNew);
            try
            {
                dl.deleteBus(busDo);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException("bus license is illegal", ex);
                //return false
            }
            return true;
        }
        //עדכון אוטובס
        public bool updatingBus(Bus busNew)
        {
            DO.Bus busDo = new DO.Bus();
            busDo = ConvertBtoD(busNew);
            try
            {
                dl.updatingBus(busDo);
            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException("bus license is illegal", ex);
                //return false
            }
            return true;
        }
        //הבאת כל הרשימה של האוטובוסים
        public IEnumerable<Bus> GetAllBuses()
        {
            var result= from Bus in dl.GetAllBuses()
                   select ConvertDtoB(Bus);
            return result;
        }
        //הבאת אוטובוס בודד
        public Bus GetOneBus(int License)
        {
            DO.Bus busDo = new DO.Bus();
            Bus busBo = new Bus();
            try
            {
                busDo = dl.GetOneBus(License);
                busBo = ConvertDtoB(busDo);
                return busBo;

            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException("bus license is illegal", ex);
            }
        }
        //הבאת אוטובס אחד עפ"י תנאי
        public IEnumerable<Bus> GetPartOfBuses(Predicate<Bus> busCondition)
        {
            try
            {
                return from item in dl.GetAllBuses()
                       let bobus = ConvertDtoB(item)
                       where busCondition(bobus)
                       select bobus;
            }

            catch (DO.BusException ex)
            {
                throw new BO.BusException("bus license is illegal", ex);
            }
        }

        #endregion Bus

        #region Line
        //המרה מ-BלD
        private DO.Line ConvertBtoD(Line LineBo)
        {
            DO.Line LineDo = new DO.Line();
            LineDo.Id = LineBo.Id;
            LineDo.FirstStation = LineBo.FirstStation;
            LineDo.LastStation = LineBo.LastStation;
            LineDo.LineNumber = LineBo.LineNumber;
            LineDo.Area = (DO.Areas)LineBo.Aera;
            return (LineDo);
        }
        //המרה מ-DלB
        private Line ConvertDtoB(DO.Line LineDo)
        {
            Line LineBo = new Line
            {
                Id = LineDo.Id,
                LineNumber = LineDo.LineNumber,
                FirstStation = LineDo.FirstStation,
                LastStation = LineDo.LastStation,
                Aera = (Areas)LineDo.Area,
                //הוספה לרשימת הלשדה חדש-רשימת תחנות בהם עובר הקו
                StationList = ((from ls in dl.GetAllLineStation()
                                let bls = ConvertDtoB(ls)
                                where bls.LineNumber == LineDo.LineNumber
                                select bls).OrderBy(linestation => linestation.LineStationIndex))
                                      .Select(item => GetOneSation(item.StationID)).ToList()
            };
            return (LineBo);
        }
        public bool addLine(Line LineNew)
        {
            DO.Line LineDo = new DO.Line();
            LineDo = ConvertBtoD(LineNew);
            try
            {
                dl.addBusLine(LineDo);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException("Line license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool updatingLine(Line LineNew)
        {
            DO.Line LineDo = new DO.Line();
            LineDo = ConvertBtoD(LineNew);
            try
            {
                dl.updatingBusLine(LineDo);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException("Line license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool deleteLine(Line LineNew)
        {
            DO.Line LineDo = new DO.Line();
            LineDo = ConvertBtoD(LineNew);
            try
            {
                dl.deleteBusLine(LineDo);
            }
            catch (DO.LineException ex)
            {
                throw new BO.LineException("Line license is illegal", ex);
                //return false
            }
            return true;
        }

        public IEnumerable<Line> GetAllBusesLine()
        {
            var result = from l in dl.GetAllBusesLine()
                         select new Line
                         {
                             Id = l.Id,
                             LineNumber = l.LineNumber,
                             Aera = (Areas)l.Area,
                             FirstStation = l.FirstStation,
                             LastStation = l.LastStation,
                             StationList = ((from ls in dl.GetAllLineStation()
                                             let bls = ConvertDtoB(ls)
                                             where bls.LineNumber == l.LineNumber
                                             select bls).OrderBy(linestation => linestation.LineStationIndex))
                                            .Select(item => GetOneSation(item.StationID)).ToList()
                         };
            return result;
        }
        public IEnumerable<Line> GetPartOfBusesLine(Predicate<Line> LineCondition)
        {
            try
            {
                return from item in dl.GetAllBusesLine()
                       let boLine = ConvertDtoB(item)
                       where LineCondition(boLine)
                       select boLine;
            }

            catch (DO.LineException ex)
            {
                throw new BO.LineException("Lune license is illegal", ex);
            }
        }

        public Line GetOneBusLine(int Id)
        {
            DO.Line LineDo = new DO.Line();
            Line LineBo = new Line();
            try
            {
                LineDo = dl.GetOneBusLine(Id);
                LineBo = ConvertDtoB(LineDo);
                return LineBo;

            }
            catch (DO.BusException ex)
            {
                throw new BO.BusException("Line license is illegal", ex);
            }
        }
        #endregion Line

        #region Station
        //המרה מ-BלD
        private DO.Station ConvertBtoD(Station StationBo)
        {
            DO.Station StationDo = new DO.Station();
            StationDo.Code = StationBo.Code;
            StationDo.Name = StationBo.Name;
            StationDo.Latitude = StationBo.Latitude;
            StationDo.Longitude = StationBo.Longitude;
            return (StationDo);
        }
        //המרה מ-DלB
        private Station ConvertDtoB(DO.Station st)
        {
            Station StationBo = new Station();

            StationBo.Code = st.Code;
            StationBo.Name = st.Name;
            StationBo.Latitude = st.Latitude;
            StationBo.Longitude = st.Longitude;
            StationBo.StationInLineList = ((from ls in dl.GetAllLineStation()
                                            let bls = ConvertDtoB(ls)
                                            where (bls.StationID == st.Code)
                                            select bls.LineNumber).ToList());
            return (StationBo);
        }

        private Station GetOneSation(int stationID)
        {

            DO.Station StationDo = new DO.Station();
            Station StationBo = new Station();
            try
            {
                StationDo = dl.GetOneStation(stationID);
                StationBo = ConvertDtoB(StationDo);
                return StationBo;

            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException("Station license is illegal", ex);
            }
        }

        public bool addStation(Station StationNew)
        {
            DO.Station StationDo = new DO.Station();
            StationDo = ConvertBtoD(StationNew);
            try
            {
                dl.addStation(StationDo);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException("Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool updatingStation(Station StationNew)
        {
            DO.Station StationDo = new DO.Station();
            StationDo = ConvertBtoD(StationNew);
            try
            {
                dl.updatingStation(StationDo);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException("Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool deleteStation(Station StationNew)
        {
            DO.Station StationDo = new DO.Station();
            StationDo = ConvertBtoD(StationNew);
            try
            {
                dl.deleteStation(StationDo);
            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException("Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public IEnumerable<Station> GetAllStation()
        {
            return from Station in dl.GetAllStation()
                   select ConvertDtoB(Station);
        }

        public IEnumerable<Station> GetPartOfStation(Predicate<Station> StationCondition)
        {
            try
            {
                return from item in dl.GetAllStation()
                       let boStation = ConvertDtoB(item)
                       where StationCondition(boStation)
                       select boStation;
            }

            catch (DO.StationException ex)
            {
                throw new BO.StationException("Station license is illegal", ex);
            }
        }

        public Station GetOneStation(int code)
        {
            DO.Station StationDo = new DO.Station();
            Station StationBo = new Station();
            try
            {
                StationDo = dl.GetOneStation(code);
                StationBo = ConvertDtoB(StationDo);
                return StationBo;

            }
            catch (DO.StationException ex)
            {
                throw new BO.StationException("Station license is illegal", ex);
            }
        }

        #endregion Station

        #region AdjacentStation
        //המרה מ-BלD
        private DO.AdjacentStation ConvertBtoD(AdjacentStation AdjacentStationBo)
        {
            DO.AdjacentStation AdjacentStationDo = new DO.AdjacentStation();
            AdjacentStationDo.Station1 = AdjacentStationBo.Station1ID;
            AdjacentStationDo.Station2 = AdjacentStationBo.Station2ID;
            AdjacentStationDo.Time = AdjacentStationBo.Time;
            AdjacentStationDo.Distance = AdjacentStationBo.Distance;
            return (AdjacentStationDo);
        }
        //המרה מ-DלB
        private AdjacentStation ConvertDtoB(DO.AdjacentStation AdjacentStationDo)
        {
            AdjacentStation AdjacentStationBo = new AdjacentStation
            {
                Station1ID = AdjacentStationDo.Station1,
                Station2ID = AdjacentStationDo.Station2,
                Time = AdjacentStationDo.Time,
                Distance = AdjacentStationDo.Distance,
            };

            return (AdjacentStationBo);
        }
        public bool addAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            DO.AdjacentStation AdjacentStationDo = new DO.AdjacentStation();
            AdjacentStationDo = ConvertBtoD(AdjacentStationNew);
            try
            {
                dl.addAdjacentStation(AdjacentStationDo);
            }
            catch (DO.AdjacentStationException ex)
            {
                throw new BO.AdjacentStationException("Adjacent Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool updatingAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            DO.AdjacentStation AdjacentStationDo = new DO.AdjacentStation();
            AdjacentStationDo = ConvertBtoD(AdjacentStationNew);
            try
            {
                dl.updatingAdjacentStation(AdjacentStationDo);
            }
            catch (DO.AdjacentStationException ex)
            {
                throw new BO.AdjacentStationException("Adjacent Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool deleteAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            DO.AdjacentStation AdjacentStationDo = new DO.AdjacentStation();
            AdjacentStationDo = ConvertBtoD(AdjacentStationNew);
            try
            {
                dl.deleteAdjacentStation(AdjacentStationDo);
            }
            catch (DO.AdjacentStationException ex)
            {
                throw new BO.AdjacentStationException("Adjacent Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public IEnumerable<AdjacentStation> GetAllAdjacentStation()
        {
            return from AdjacentStation in dl.GetAllAdjacentStation()
                   select ConvertDtoB(AdjacentStation);
        }
        public IEnumerable<AdjacentStation> GetPartOfAdjacentStation(Predicate<AdjacentStation> AdjacentStationCondition)
        {
            try
            {
                return from item in dl.GetAllAdjacentStation()
                       let boGetAllAdjacentStation = ConvertDtoB(item)
                       where AdjacentStationCondition(boGetAllAdjacentStation)
                       select boGetAllAdjacentStation;
            }

            catch (DO.AdjacentStationException ex)
            {
                throw new BO.AdjacentStationException("Adjacent Station license is illegal", ex);
            }
        }

        public AdjacentStation GetOneAdjacentStation(int Station1, int Station2)
        {
            DO.AdjacentStation AdjacentStationDo = new DO.AdjacentStation();
            AdjacentStation AdjacentStationBo = new AdjacentStation();
            try
            {
                AdjacentStationDo = dl.GetOneAdjacentStation(Station1, Station2);
                AdjacentStationBo = ConvertDtoB(AdjacentStationDo);
                return AdjacentStationBo;

            }
            catch (DO.AdjacentStationException ex)
            {
                throw new BO.AdjacentStationException("Adjacent Station license is illegal", ex);
            }
        }
        //מביא את התחנה הסמוכה  לתחנה הספציפית
        public AdjacentStation GetOneAdjacentStation2(int Station)
        {
            
            AdjacentStation AdjacentStationBo = new AdjacentStation();
            AdjacentStationBo = (AdjacentStation)(from ls in GetAllAdjacentStation()
                                                  where ((ls.Station1ID == Station)||(ls.Station2ID == Station))
                                                  select ls);
            return (AdjacentStationBo);
        }
        #endregion AdjacentStation

        #region LineStation

        private LineStation ConvertDtoB(DO.LineStation linestation)
        {
            var LineStationBo = new LineStation();

            LineStationBo.LineNumber = linestation.LineNumber;
            LineStationBo.StationID = linestation.Station;
            LineStationBo.LineStationIndex = linestation.LineStationIndex;
            LineStationBo.PrevStation = linestation.PrevStation;
            LineStationBo.NextStation = linestation.NextStation;
            AdjacentStation AdjacentStationBo = GetOneAdjacentStation(LineStationBo.PrevStation,LineStationBo.StationID);
            LineStationBo.TimeFromPrevious = AdjacentStationBo.Time;
            LineStationBo.DistanceFromPrevious = AdjacentStationBo.Distance;
            AdjacentStationBo = GetOneAdjacentStation(LineStationBo.StationID, LineStationBo.NextStation);
            LineStationBo.TimetoNext = AdjacentStationBo.Time;
            LineStationBo.DistancetoNext = AdjacentStationBo.Distance;
            return LineStationBo;
        }
        //המרה מ-BלD
        private DO.LineStation ConvertBtoD(LineStation LineStation)
        {
            DO.LineStation LineStationDo = new DO.LineStation();
            LineStationDo.Station= LineStation.StationID;
            LineStationDo.LineNumber = LineStation.LineNumber;
            LineStationDo.LineStationIndex = LineStation.LineStationIndex;
             LineStationDo.PrevStation = LineStation.PrevStation;
            LineStationDo.NextStation = LineStation.NextStation;
            return (LineStationDo);
        }

        public bool addLineStation(LineStation LineStationNew)
        {
            DO.LineStation LineStationDo = new DO.LineStation();
            LineStationDo = ConvertBtoD(LineStationNew);
            try
            {
                dl.addLineStation(LineStationDo);
            }
            catch (DO.LineStationDException ex)
            {
                throw new BO.LineStationBException("Line Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool updatingLineStation(LineStation LineStationNew)
        {
            DO.LineStation LineStationDo = new DO.LineStation();
            LineStationDo = ConvertBtoD(LineStationNew);
            try
            {
                dl.updatingLineStation(LineStationDo);
            }
            catch (DO.LineStationDException ex)
            {
                throw new BO.LineStationBException("Line Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public bool deleteLineStation(LineStation LineStationNew)
        {
            DO.LineStation LineStationDo = new DO.LineStation();
            LineStationDo = ConvertBtoD(LineStationNew);
            try
            {
                dl.deleteLineStation(LineStationDo);
            }
            catch (DO.LineStationDException ex)
            {
                throw new BO.LineStationBException("Line Station license is illegal", ex);
                //return false
            }
            return true;
        }

        public IEnumerable<LineStation> GetAllLineStation()
        {
            return from LineStation in dl.GetAllLineStation()
                   select ConvertDtoB(LineStation);
        }

        public IEnumerable<LineStation> GetPartOfLineStation(Predicate<LineStation> LineStationCondition)
        {
            try
            {
                return from item in dl.GetAllLineStation()
                       let boGetAllLineStation = ConvertDtoB(item)
                       where LineStationCondition(boGetAllLineStation)
                       select boGetAllLineStation;
            }

            catch (DO.LineStationDException ex)
            {
                throw new BO.LineStationBException("Line Station license is illegal", ex);
            }
        }

        public LineStation GetOneLineStation(int LineNumber)
        {
            DO.LineStation LineStationDo = new DO.LineStation();
            LineStation LineStationBo = new LineStation();
            try
            {
                LineStationDo = dl.GetOneLineStation(LineNumber);
                LineStationBo = ConvertDtoB(LineStationDo);
                return LineStationBo;

            }
            catch (DO.LineStationDException ex)
            {
                throw new BO.LineStationBException("Line Station license is illegal", ex);
            }
        }
        #endregion LineStation

        #region users
        private DO.User ConvertBtoD(User user)
        {
            DO.User userDO = new DO.User
            {
                UserName = user.UserName,
                Password = user.Password,
            };
            return userDO;
        }

        private User convertoBO(DO.User user)
        {
            return new User
            {
                UserName = user.UserName,
                Password = user.Password,
            };
        }
        public IEnumerable<User> GetAllUsersBO()//הדפסת כל המשתמשים
        {
            return from user in dl.GetAllUser()
                   select convertoBO(user);
        }
        public User GetUserBO(string userName)//קבלת פרטי משתמש בודד
        {
            User result = new User();
            DO.User userDO;
            try
            {
                userDO = dl.GetOneUser(userName);
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException("userName not found", ex);
            }
            result = convertoBO(userDO);
            return result;
        }
        //הוספה, עדכון ומחיקת משתמש
        public bool addUser(User user)
        {
            bool result;
            try
            {
                result = dl.addUser(ConvertBtoD(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException("שם המשתמש בשימוש כבר", ex);
            }
            return result;
        }
        public bool updateUser(User user)
        {
            bool result;
            try
            {
                result = dl.updatingUser(ConvertBtoD(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException("The userName " + user.UserName + " not found", ex);
            }
            return result;
        }
        public bool deleteUser(User user)
        {
            bool result;
            try
            {
                result = dl.deleteUser(ConvertBtoD(user));
            }
            catch (DO.UserException ex)
            {
                throw new BO.UserException("Does not exist in the system", ex);
            }
            return result;
        }
        
        public bool existingUser(string userName, string passWord)
        {
            DO.User user1 = dl.GetAllUser().ToList().Find(p => p.UserName == userName && p.Password == passWord);

            if (user1 != null)
                return true;
            else
                throw new BO.UserException("One or more Does not exist in the system");
        }
        #endregion

    }
}
