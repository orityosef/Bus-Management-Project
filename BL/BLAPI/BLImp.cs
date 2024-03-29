﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using BLAPI;
using BL.BO;

namespace BL.BLAPI
{
    class BLImp : IBL //internal
    {
        static IDL dl = DLFactory.GetDL();

        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion

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
            return from bus in dl.GetAllBuses()
                   select ConvertDtoB(bus);
            //var result = from Bus in dl.GetAllBuses().ToList()
            //             select ConvertDtoB(Bus);
            // return result;
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
        //שליחת אוטובוס לטיפול ותדלוק
        public void Refuelling(string license)
        {
            IEnumerable<DO.Bus> buses = dl.GetAllBuses();
            if (!buses.Any(item => item.LicenseNum.ToString() == license))
            {
                throw new ArgumentNullException("bus not found");
            }
            DO.Bus busD = new DO.Bus();
            foreach (DO.Bus bus in buses)
            {
                if (bus.LicenseNum.ToString() == license)
                    busD = bus;
            }
            if (busD.FuelRemain == 1200)
            {
                throw new BO.BusException("this bus Refuel is  full");
            }

            busD.Status = DO.Status.ReadyToGo;//אחרי זה צריך לשנות את הסטטוס בחזרה למוכן
            busD.Refuel = 1200;//התדלוק עצמו
            dl.updatingBus(busD);
        }
        public void Treatment(string license)
        {
            IEnumerable<DO.Bus> buses = dl.GetAllBuses();
            if (!buses.Any(item => item.LicenseNum.ToString() == license))
            {
                throw new ArgumentNullException("bus not found");
            }
            DO.Bus busD = new DO.Bus();
            foreach (DO.Bus bus in buses)
            {
                if (bus.LicenseNum.ToString() == license)
                    busD = bus;
            }
            busD.Status = DO.Status.Treatment;
            busD.Status = DO.Status.ReadyToGo;//אחרי זה צריך לשנות את הסטטוס בחזרה למוכן
            busD.TotalTrip = 0;
            dl.updatingBus(busD);
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
            DO.Station x1 = dl.GetOneStation(LineDo.FirstStation);
            DO.Station x2 = dl.GetOneStation(LineDo.LastStation);
            Line LineBo = new Line
            {
                Id = LineDo.Id,
                LineNumber = LineDo.LineNumber,
                FirstStation = LineDo.FirstStation,
                FirstStationN = x1.Name,
                LastStationN = x2.Name,
                LastStation = LineDo.LastStation,
                Aera = (Areas)LineDo.Area,
                //הוספה לרשימת הלשדה חדש-רשימת תחנות בהם עובר הקו
                StationList = ((from ls in dl.GetAllLineStation()
                                let bls = ConvertDtoB(ls)
                                where bls.LineNumber == LineDo.LineNumber
                                select bls).OrderBy(linestation => linestation.LineStationIndex))
                                      .Select(item => GetOneSation(item.StationID)).ToList()
            };
            IEnumerable<DO.LineStation> listLineStations = dl.getPartOfLineStations(item => item.LineNumber == LineDo.LineNumber);//רשימה של תחנות קו המתאימות לקו הזה
            IEnumerable<DO.LineStation> listStationInLineOrder =//נשמרות כל התחנות קו של הקו הזה בצורה ממוינת
               from lineStation in listLineStations
               orderby lineStation.LineStationIndex
               select lineStation;
            IEnumerable<DO.AdjacentStation> pairs = dl.GetAllAdjacentStation();
            List<LineStation> listStationTypeCorrect = new List<LineStation>();
            DO.LineStation prev = null;
            DO.LineStation current = null;
            bool first = true;
            //הלולאה הזאת עוברת על כל תחנות קו שמקושרות לקו הזה, ויוצרת רשימה של "תחנות בקו" שתוכנס לשדה המבוקש
            foreach (DO.LineStation lineStationNext in listStationInLineOrder)
            {
                prev = current;//מקוות שזה המקום הנכון
                current = lineStationNext;
                if (!first)
                {
                    LineStation forNow = new LineStation();
                    forNow.StationID = lineStationNext.Station;
                    forNow.LineStationIndex = lineStationNext.LineStationIndex;
                    forNow.NameStation = dl.GetOneStation(lineStationNext.Station).Name;
                    TimeSpan count = new TimeSpan(0, 0, 0);
                    for (int i = forNow.LineStationIndex; i > 1; i--)//חישוב זמן הנסיעה של התחנה הנוכחית מתחנת המוצא
                    {
                        count += dl.GetOneAdjacentStation(listStationInLineOrder.ToArray()[i - 1].Station, listStationInLineOrder.ToArray()[i - 2].Station).Time;
                    }
                    forNow.TimeFromFirstStation = count;
                    if (dl.GetOneAdjacentStation(current.Station, prev.Station) != null)
                    {
                        forNow.Distance = dl.GetOneAdjacentStation(current.Station, prev.Station).Distance;//הצבת המרחק מתוך זוג התחנות העוקבות בשדה מרחק של התחנה שלנו מקודמתה
                        forNow.Time = dl.GetOneAdjacentStation(current.Station, prev.Station).Time;//הצבת הזמן נסיעה מתוך זוג התחנות העוקבות בשדה זמן נסיעה של התחנה שלנו מקודמתה
                    }
                    else
                    {
                        forNow.Distance = 0;
                        forNow.Time = new TimeSpan(0, 0, 0);
                    }
                    listStationTypeCorrect.Add(forNow);
                }
                else//במקרה שזו התחנה הראשונה, נאתחל את המרחק והזמן לאפסים
                {
                    listStationTypeCorrect.Add(new LineStation
                    {
                        StationID = lineStationNext.Station,
                        LineNumber = lineStationNext.LineStationIndex,
                        NameStation = dl.GetOneStation(lineStationNext.Station).Name,
                        Distance = 0,
                        Time = new TimeSpan(0, 0, 0),
                        TimeFromFirstStation = new TimeSpan(0, 0, 0)
                    });
                }
                first = false;
            }
            LineBo.ListOfStations = listStationTypeCorrect;
            IEnumerable<BusOnTrip> trips =
                from trip in dl.getPartOfLineTrip(item => item.IdentifyNumber == LineDo.Id)
                orderby trip.TripStart
                select ConvertDtoB(trip);
            LineBo.ListOfTrips = trips;
            return LineBo;
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

        //public IEnumerable<Line> GetAllBusesLine()
        //{
        //    var result = from l in dl.GetAllBusesLine()
        //                 select new Line
        //                 {
        //                     Id = l.Id,
        //                     LineNumber = l.LineNumber,
        //                     Aera = (Areas)l.Area,
        //                     FirstStation = l.FirstStation,
        //                     LastStation = l.LastStation,
        //                     StationList = ((from ls in dl.GetAllLineStation()
        //                                     let bls = ConvertDtoB(ls)
        //                                     where bls.LineNumber == l.LineNumber
        //                                     select bls).OrderBy(linestation => linestation.LineStationIndex))
        //                                    .Select(item => GetOneSation(item.StationID)).ToList()
        //                 };
        //    return result;

        //}
        public IEnumerable<Line> GetAllBusesLine()
        {
            return from busLine in dl.GetAllBusesLine()
                   orderby busLine.LineNumber
                   select ConvertDtoB(busLine);
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
        private Station ConvertDtoB(DO.Station busStation)
        {
            Station busStationBO = new Station
            {
                Code = busStation.Code,
                Latitude = busStation.Latitude,
                Longitude = busStation.Longitude,
                Name = busStation.Name,
              
            };
            try
            {
                IEnumerable<DO.LineStation> listLineStations = dl.getPartOfLineStations(item => item.Station== busStation.Code);//רשימה של תחנות קו המתאימות לתחנה הזאת
                IEnumerable<LineInStation> listOfLineInStation =
                from lineStation in listLineStations
                from BusLine1 in dl.GetAllBusesLine()
                where lineStation.Id == BusLine1.Id
                let result = new LineInStation
                {
                    IdentifyNumber = BusLine1.Id,
                    LineNumber = BusLine1.LineNumber,
                    LastStationName = dl.GetOneStation(BusLine1.LastStation).Name,
                    LastStationNum = BusLine1.LastStation
                }
                select result;
                busStationBO.ListOfLines = listOfLineInStation;
                return busStationBO;
            }
            catch//אין קווים שעוברים בתחנה
            {
                busStationBO.ListOfLines = null;
                return busStationBO;
            }
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
                throw new StationException("Station license is illegal", ex);
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
                                                  where ((ls.Station1ID == Station) || (ls.Station2ID == Station))
                                                  select ls);
            return (AdjacentStationBo);
        }
        #endregion AdjacentStation

        #region LineStation

        private LineStation ConvertDtoB(DO.LineStation linestation)
        {
            var LineStationBo = new LineStation();
            LineStationBo.Id = linestation.Id;
            LineStationBo.LineNumber = linestation.LineNumber;
            LineStationBo.StationID = linestation.Station;
            LineStationBo.LineStationIndex = linestation.LineStationIndex;
            LineStationBo.PrevStation = linestation.PrevStation;
            LineStationBo.NextStation = linestation.NextStation;
            if ((LineStationBo.PrevStation != 0) && (LineStationBo.NextStation != 0))
            {
                AdjacentStation AdjacentStationBo = GetOneAdjacentStation(LineStationBo.PrevStation, LineStationBo.StationID);
                LineStationBo.TimeFromFirstStation = AdjacentStationBo.Time;
                LineStationBo.Distance = AdjacentStationBo.Distance;
                AdjacentStationBo = GetOneAdjacentStation(LineStationBo.StationID, LineStationBo.NextStation);
                LineStationBo.Time = AdjacentStationBo.Time;
            }
            if ((LineStationBo.PrevStation == 0))
            {
                LineStationBo.TimeFromFirstStation = new TimeSpan(0, 0, 0);
                LineStationBo.Distance = 0;
                AdjacentStation AdjacentStationBo = GetOneAdjacentStation(LineStationBo.StationID, LineStationBo.NextStation);
                LineStationBo.Time = AdjacentStationBo.Time;

            }
            if (LineStationBo.NextStation == 0)
            {
                AdjacentStation AdjacentStationBo = GetOneAdjacentStation(LineStationBo.PrevStation, LineStationBo.StationID);
                LineStationBo.TimeFromFirstStation = AdjacentStationBo.Time;
                LineStationBo.Distance = AdjacentStationBo.Distance;
                LineStationBo.Time = new TimeSpan(0, 0, 0);


            }
            return LineStationBo;
        }
        //המרה מ-BלD
        private DO.LineStation ConvertBtoD(LineStation LineStation)
        {
            DO.LineStation LineStationDo = new DO.LineStation();
            LineStationDo.Id = LineStation.Id;
            LineStationDo.Station = LineStation.StationID;
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

        #region MiniStation
        public IEnumerable<MiniStation> GetAllMiniStations()//תצוגה מינימלית של תחנה עבור הקומבובוקס
        {
            IEnumerable<MiniStation> result =
                from station in GetAllStation()
                select new MiniStation { CodeStation = station.Code, NameStation = station.Name };
            return result;
        }
        public IEnumerable<MiniStation> GetListMiniStationsByLine(Line line)//מחזירה את רשימת המיני תחנות של קו ספציפי
        {
            IEnumerable<MiniStation> result =
                from station in line.StationList
                select new MiniStation { CodeStation = station.Code, NameStation = station.Name };
            return result;
        }

        #endregion


        #region BusOnTrip
        private DO.BusOnTrip ConvertBtoD(BusOnTrip lineTrip)
        {
            DO.BusOnTrip lineTripDAO = new DO.BusOnTrip
            {
                IdentifyNumber = lineTrip.IdentifyNumber,
                TripStart = lineTrip.TripStart,
            };
            return lineTripDAO;
        }
        private BusOnTrip ConvertDtoB(DO.BusOnTrip lineTrip)
        {
            BusOnTrip result = new BusOnTrip
            {
                IdentifyNumber = lineTrip.IdentifyNumber,
                TripStart = lineTrip.TripStart,
            };
            return result;
        }
        public bool addLineTrip(BusOnTrip lineTrip)
        {
            bool result;
            try
            {
                result = dl.addLineTrip(ConvertBtoD(lineTrip));
            }
            catch (DO.BusOnTripException ex)
            {
                throw new BusOnTripException("יציאת הקו כבר קיימת במערכת", ex);
            }
            return result;
        }
        public bool deleteLineTrip(BusOnTrip lineTrip)
        {
            bool result;
            try
            {
                result = dl.deleteLineTrip(ConvertBtoD(lineTrip));
            }
            catch (DO.BusOnTripException ex)
            {
                throw new BusOnTripException("Does not exist in the system", ex);
            }
            return result;
        }
        #endregion

        //פונקציות עבור לוח אלקטרוני

        // מקבל 2 תחנות ומחזיר את הקו שעובר  בניהם לא מחזיר כלום אם לא קיים קו
        public List<WayForPass> GetRelevantWays(int codeStation1, int codeStation2)
        {
            List<WayForPass> result = new List<WayForPass>();

            foreach (Line line in GetAllBusesLine())
            {
                bool ifFirstIn = false;
                int LocationFirst = 0;
                bool ifLastIn = false;
                int LocationLast = 0;
                // LocationFirst = (from station in line.ListOfStations
                //         where station.StationID == codeStation1
                //         select station.LineStationIndex).FirstOrDefault();
                //ifFirstIn = (LocationFirst != 0);
                foreach (var station in line.ListOfStations)
                {
                    if (station.StationID == codeStation1)
                    {
                        ifFirstIn = true;
                        LocationFirst = station.LineStationIndex;
                        break;
                    }
                }
                foreach (var station in line.ListOfStations)
                {
                    if (station.StationID == codeStation2)
                    {
                        ifLastIn = true;
                        LocationLast = station.LineStationIndex;
                        break;
                    }
                }
                if (ifFirstIn == true && ifLastIn == true && LocationFirst < LocationLast)//אם שתי התחנות קיימות בקו ותחנת המוצא לפני תחנת היעד
                {
                    TimeSpan count = new TimeSpan(0, 0, 0);
                    for (int i = LocationLast - 1; i >= LocationFirst; i--)//עובר מהתחנת יעד עד תחנת המוצא אחורה
                    {
                        count += line.ListOfStations.ToArray()[i].Time;//סופר את זמן הנסיעה של המסלול הזה
                    }
                    result.Add(new WayForPass { LineNumber = line.LineNumber, TimeOfTrip = count });
                }
            }
            IEnumerable<WayForPass> orderList =
                from way in result
                orderby way.TimeOfTrip
                select way;
            return orderList.ToList();

        }
        //מקבל את הזמן עכשיו ומספר תחנה
        //מחזיר רשימה של הקויים הרלוונטים לתחנה שעוברים בה ואת הזמן עד שהם יגיעו
        public IEnumerable<LineTimingBO> GetLineTimingsPerStation(Station cuurentStation, TimeSpan now)
        {
            List<LineTimingBO> result = new List<LineTimingBO>();
            if (cuurentStation.ListOfLines != null)
            {

                foreach (LineInStation line in cuurentStation.ListOfLines)//נעבור על כל הקווים שעוברים בתחנה
                {
                    LineTimingBO lineTiming = new LineTimingBO();
                    lineTiming.IdentifyNumber = line.IdentifyNumber;
                    lineTiming.LineNumber = line.LineNumber;
                    lineTiming.LastStationName = line.LastStationName;
                    Line curLine = GetOneBusLine(line.IdentifyNumber);
                    TimeSpan TimeTripFromStart = new TimeSpan(0, 0, 0);

                    foreach (LineStation stationInLine in curLine.ListOfStations)//חישוב כמה זמן לוקח לקו הספציפי להגיע לתחנה שלנו
                    {
                        if (stationInLine.StationID == cuurentStation.Code)
                            TimeTripFromStart = stationInLine.TimeFromFirstStation;
                    }
                    try
                    {
                        //נוצרת רשימה של יציאות הקו הרלוונטיות, כלומר שייכות לקו הנוכחי ועוברות בתחנה בחצי השעה הקרובה
                        List<DO.BusOnTrip> relevantTripToLine = dl.getPartOfLineTrip(item => item.IdentifyNumber == line.IdentifyNumber && item.TripStart + TimeTripFromStart > now && item.TripStart + TimeTripFromStart < now + new TimeSpan(0, 30, 0)).ToList();
                        foreach (DO.BusOnTrip lineTrip in relevantTripToLine)
                        {
                            lineTiming.TripStart = lineTrip.TripStart;
                            lineTiming.ExpectedTimeTillArrive = lineTrip.TripStart + TimeTripFromStart;
                            lineTiming.MoreHowMinutesCome = (lineTiming.ExpectedTimeTillArrive - now).Minutes;
                            result.Add(lineTiming);
                        }
                    }
                    catch { }//במקרה של תחנה שלא עוברים בה קווים בחצי השעה הקרובה
                }
            }
            return result;
        }


    }
}
