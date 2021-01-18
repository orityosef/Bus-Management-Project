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

        public IEnumerable<Bus> GetAllBuses()
        {
            return from Bus in dl.GetAllBuses()
                   select ConvertDtoB(Bus);
        }

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

        public IEnumerable<Bus> GetPartOfBuses(Predicate<Bus> busCondition)
        {
            //please remove GetPartOfBuses from DalObject vechu
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

        public IEnumerable<Bus> GetPartOfBuses(Predicate<DO.Bus> BusCondition)
        {
            throw new NotImplementedException();
        }

        public bool addLine(Line LineNew)
        {
            throw new NotImplementedException();
        }

        public bool updatingLine(Line LineNew)
        {
            throw new NotImplementedException();
        }

        public bool deleteLine(Line LineNew)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Line> GetAllBusesLine()
        {
            var result = from l in dl.GetAllBusesLine()
                   select new Line
                   {
                       Id = l.Id,
                       LineNumber = l.LineNumber,
                       Aera = (Areas)l.Aera,
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

        private Station GetOneSation(int stationID)
        {
            return ConvertDtoB(dl.GetOneStation(stationID));
        }

        private Station ConvertDtoB(DO.Station st)
        {
            return new Station
            {
                Code = st.Code,
                Name = st.Name,
                Latitude = st.Latitude,
                Longitude = st.Longitude
            };
        }

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


    public IEnumerable<Line> GetPartOfBusesLine(Predicate<Line> LineCondition)
        {
            throw new NotImplementedException();
        }

        public Line GetOneBusLine(string Id)
        {
            throw new NotImplementedException();
        }

        #endregion Bus



    }
}
