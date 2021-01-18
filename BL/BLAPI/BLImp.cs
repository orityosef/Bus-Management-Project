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
            return from Bus in dl.GetAllBuses()
                   select ConvertDtoB(Bus);
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
                                      .Select( item =>GetOneSation(item.StationID)).ToList()
                   };
            return result;
        }
        public IEnumerable<Line> GetPartOfBusesLine(Predicate<Line> LineCondition)
        {
            try
            {
                return from item in dl.GetAllBusesLine()
                       let bobus = ConvertDtoB(item)
                       where LineCondition(bobus)
                       select bobus;
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
            return ConvertDtoB(dl.GetOneStation(stationID));
        }

        public bool addStation(Station StationNew)
        {
            throw new NotImplementedException();
        }

        public bool updatingStation(Station StationNew)
        {
            throw new NotImplementedException();
        }

        public bool deleteStation(Station StationNew)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetAllStation()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetPartOfStation(Predicate<Station> StationCondition)
        {
            throw new NotImplementedException();
        }

        public Line GetOneStation(int code)
        {
            throw new NotImplementedException();
        }
        #endregion Station
        

        #region LineStation
        private LineStation ConvertDtoB(DO.LineStation linestation)
        {
            var result = new LineStation
            {
                LineNumber = linestation.LineNumber,
                StationID = linestation.Station,
                LineStationIndex = linestation.LineStationIndex

            };
            return result;
        }
        #endregion LineStation
    }
}
