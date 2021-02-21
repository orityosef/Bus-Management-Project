﻿using DLAPI;

using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{
    public class DLXML : IDL
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use

        #endregion

        #region DS XML File
        string busPath = @"XMLBus.xml"; //XElement
        string linePath = @"XMLLine.xml"; //XMLSerializer
        string stationPath = @"XMLStation.xml"; //XMLSerializer
        string lineStationPath = @"XMLlineStation.xml"; //XMLSerializer
        string AdjacentStationPath = @"XMLAdjacentStation.xml"; //XElement
        string userPath = @"XMLUser.xml"; //XMLSerializer
        #endregion

        #region Bus
        public bool addBus(Bus busNew)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busPath);

            XElement bus1 = (from p in busesRootElem.Elements()
                             where p.Element("LicenseNum").Value == busNew.LicenseNum.ToString()
                             select p).FirstOrDefault();

            if (bus1 != null)
                throw new BusException("license exists allready");

            XElement busElem = new XElement("BusDAO",
                                   new XElement("LicenseNum", busNew.LicenseNum),//העתקה עמוקה
                                   new XElement("Fromdate", busNew.Fromdate),
                                   new XElement("TotalTrip", busNew.TotalTrip),
                                   new XElement("FuelRemain", busNew.FuelRemain),
                                   new XElement("Status", busNew.Status));

            busesRootElem.Add(busElem);

            XMLTools.SaveListToXMLElement(busesRootElem, busPath);
            return true;
        }


        public bool deleteBus(Bus busNew)
        {

            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busPath);

            XElement bus1 = (from p in busesRootElem.Elements()
                             where p.Element("LicenseNum").Value == busNew.LicenseNum.ToString()
                             select p).FirstOrDefault();

            if (bus1 != null)
            {
                bus1.Remove();
                XMLTools.SaveListToXMLElement(busesRootElem, busPath);
                return true;
            }
            else
                throw new BusException("Does not exist in the system");
        }

        public bool updatingBus(Bus busNew)
        {

            List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
            Bus bus1 = ListBus.Find(p => p.LicenseNum == busNew.LicenseNum);
            if (bus1 != null)
            {
                
                    Bus current = GetOneBus(busNew.LicenseNum);//שמירה על הערכים הקודמים של הקו
                    busNew.LicenseNum = current.LicenseNum;
                    busNew.Fromdate = current.Fromdate;
                    busNew.TotalTrip = current.TotalTrip;
                    busNew.FuelRemain = current.FuelRemain;
                    busNew.Status = current.Status;
                    ListBus.Remove(bus1);
                    ListBus.Add(busNew); //no nee to Clone()
                }

                 else
                    throw new DO.BusException("The Identify-Number ");
                XMLTools.SaveListToXMLSerializer(ListBus, busPath);
                return true;
            }
        public Bus GetOneBus(int License)
        {
            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busPath);

            Bus p = (from bus in busesRootElem.Elements()
                     where bus.Element("LicenseNum").Value == License.ToString()
                     select new Bus()
                     {
                         LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                         Fromdate = DateTime.Parse(bus.Element("Fromdate").Value),
                         TotalTrip = Int32.Parse(bus.Element("TotalTrip").Value),
                         FuelRemain = Int32.Parse(bus.Element("FuelRemain").Value),
                         Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                     }
                        ).FirstOrDefault();
            if (p == null)
                throw new DO.BusException("license not found");
            return p;
        }
        public IEnumerable<Bus> GetAllBuses()
        {

            XElement busesRootElem = XMLTools.LoadListFromXMLElement(busPath);

            return (from bus in busesRootElem.Elements()
                    select new Bus()
                    {
                        Status = (Status)Enum.Parse(typeof(Status), bus.Element("Status").Value),
                        LicenseNum = Int32.Parse(bus.Element("LicenseNum").Value),
                        Fromdate = DateTime.Parse(bus.Element("Fromdate").Value),
                        TotalTrip = Int32.Parse(bus.Element("TotalTrip").Value),
                        FuelRemain = Int32.Parse(bus.Element("FuelRemain").Value),
                      
                       
                    }
                   );
        }
        //List<Bus> ListBuss = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
        //    return from Bus in ListBuss
        //           select Bus;

        // }


        #endregion Bus

        #region Line
        public bool addBusLine(Line busLineNew)
        {
            List<DO.Line> listLine = XMLTools.LoadListFromXMLSerializer<DO.Line>(linePath);//שליפת הקובץ שמכיל את רשימת הקווים
            List<LineStation> ListLineStation = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            if (listLine.FirstOrDefault(s => s.LineNumber == busLineNew.LineNumber) != null)
                throw new LineException("Identify-Number-Line exists allready");
            XElement dlConfig = XElement.Load(@"config.xml");
            int identifyNumber = int.Parse(dlConfig.Element("Id").Value); ;
            busLineNew.Id = identifyNumber++;
            dlConfig.Element("Id").Value = identifyNumber.ToString();
            dlConfig.Save(@"config.xml");
            listLine.Add(busLineNew); //no need to Clone()
            ListLineStation.Add(new LineStation { Station = busLineNew.FirstStation, LineNumber = busLineNew.LineNumber, LineStationIndex = 1 });
            ListLineStation.Add(new LineStation { Station = busLineNew.LastStation, LineNumber = busLineNew.LineNumber, LineStationIndex = 2 });
            XMLTools.SaveListToXMLSerializer(listLine, linePath);
            XMLTools.SaveListToXMLSerializer(ListLineStation, lineStationPath);
            return true;
        }

        public bool updatingBusLine(Line busLine)
        {
            List<Line> ListBusLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);
            Line line = ListBusLines.Find(p => p.LineNumber == busLine.LineNumber);
            if (line != null)
            {
                Line currentLine = GetOneBusLine(busLine.LineNumber);//שמירה על הערכים הקודמים של הקו
                busLine.LineNumber = currentLine.LineNumber;
                busLine.FirstStation = currentLine.FirstStation;
                busLine.LastStation = currentLine.LastStation;
                ListBusLines.Remove(line);
                ListBusLines.Add(busLine); //no need to Clone()
            }
            else
                throw new DO.LineException("The Identify-Number-Line " + busLine.LineNumber + " not found");
            XMLTools.SaveListToXMLSerializer(ListBusLines, linePath);
            return true;
        }
        public bool deleteBusLine(Line busLine)
        {
            List<Line> ListBusLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);
            Line line = ListBusLines.Find(p => p.LineNumber == busLine.LineNumber);
            if (line != null)
            {
                ListBusLines.Remove(line);
            }
            else
                throw new LineException("Does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListBusLines, linePath);
            return true;
        }

        public IEnumerable<Line> GetAllBusesLine()
        {
            List<Line> ListBusLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            return from line in ListBusLines
                   select line; //no need to Clone()
        }


        public Line GetOneBusLine(int LineNumber)
        {
            List<Line> ListBusLines = XMLTools.LoadListFromXMLSerializer<Line>(linePath);

            DO.Line line = ListBusLines.Find(p => p.LineNumber == LineNumber);
            if (line != null)
                return line; //no need to Clone()
            else
                throw new DO.LineException("The Identify-Number-Line " + LineNumber + " not found");
        }

        #endregion Line

        #region Station
        public bool addStation(Station StationNew)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            if (ListStations.FirstOrDefault(s => s.Code == StationNew.Code) != null)
                throw new StationException("Code Station exists allready");
            ListStations.Add(StationNew); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);
            return true;
        }

        public bool updatingStation(Station StationNew)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station sta = ListStations.Find(p => p.Code == StationNew.Code);
            if (sta != null)
            {
                ListStations.Remove(sta);
                ListStations.Add(StationNew); //no nee to Clone()
            }
            else
                throw new DO.StationException("The Code Station " + StationNew.Code + " not found");
            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);
            return true;
        }

        public bool deleteStation(Station StationNew)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            Station sta = ListStations.Find(p => p.Code == StationNew.Code);
            if (sta != null)
            {

                ListStations.Remove(sta);
                //מחיקת האובייקטים של תחנות עוקבות שקשורות לתחנה הזאת
            }
            else
                throw new StationException("Does not exist in the system");

            XMLTools.SaveListToXMLSerializer(ListStations, stationPath);
            return true;
        }

        public IEnumerable<Station> GetAllStation()
        {
            List<Station> ListBusStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);
            return from station in ListBusStations
                   select station; //no need to Clone()
        }



        public Station GetOneStation(int code)
        {
            List<Station> ListBusStations = XMLTools.LoadListFromXMLSerializer<Station>(stationPath);

            DO.Station sta = ListBusStations.Find(p => p.Code == code);
            if (sta != null)
                return sta; //no need to Clone()
            else
                throw new DO.StationException("The Code Station " + code + " not found");
        }
        #endregion Station


        #region LineStation
        public bool addLineStation(LineStation LineStationNew)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            if (ListLineStations.FirstOrDefault(mishehu => mishehu.LineNumber == LineStationNew.LineNumber && mishehu.Station == LineStationNew.Station) != null)
                throw new LineStationDException("The station already exists on the line");
            ListLineStations.Add(LineStationNew); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
            return true;
        }
        public IEnumerable<LineStation> getPartOfLineStations(Predicate<LineStation> LineStationDAOCondition)
        {

            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            IEnumerable<LineStation> TempLineStationDAO = from LineStation item in ListLineStations
                                                          where LineStationDAOCondition(item)
                                                          select item;//no need to Clone()
            if (TempLineStationDAO.Count() == 0)
                throw new LineStationDException("There are no line stations that meet the condition");
            return TempLineStationDAO;
        }

        public bool updatingLineStation(LineStation LineStationNew)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            LineStation sta = ListLineStations.Find(mishehu => mishehu.LineNumber == LineStationNew.LineNumber && mishehu.Station == LineStationNew.Station);
            if (sta != null)
            {
                ListLineStations.Remove(sta);
                ListLineStations.Add(LineStationNew); //no nee to Clone()
            }
            else
                throw new DO.LineStationDException("The Station number " + LineStationNew.Station + " not found in the line " + LineStationNew.LineNumber);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
            return true;
        }

        public bool deleteLineStation(LineStation LineStationNew)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            LineStation sta = ListLineStations.Find(item => item.LineNumber == LineStationNew.LineNumber && item.Station == LineStationNew.Station);
            if (sta != null)
            {
                ListLineStations.Remove(sta);
            }
            else
                throw new LineStationDException("Does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationPath);
            return true;
        }

        public IEnumerable<LineStation> GetAllLineStation()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);
            return from station in ListLineStations
                   select station; //no need to Clone()
        }


        public LineStation GetOneLineStation(int LineNumber)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationPath);

            DO.LineStation sta = ListLineStations.Find(p => p.LineNumber == LineNumber);
            if (sta != null)
                return sta; //no need to Clone()
            else
                throw new DO.LineStationDException("The Station number " + LineNumber);
        }

        #endregion LineStation


        #region User
        public bool addUser(User UserNew)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);
            if (ListUsers.FirstOrDefault(mishehu => mishehu.UserName == UserNew.UserName) != null)
                throw new UserException("שם המשתמש כבר קיים");
            ListUsers.Add(UserNew); //no need to Clone()
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);
            return true;
        }

        public bool updatingUser(User UserNew)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);
            User sta = ListUsers.Find(mishehu => mishehu.UserName == UserNew.UserName);
            if (sta != null)
            {
                ListUsers.Remove(sta);
                ListUsers.Add(UserNew); //no need to Clone()
            }
            else
                throw new DO.UserException("The UserName " + UserNew.UserName + " not found");
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);
            return true;
        }

        public bool deleteUser(User UserNew)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);
            User sta = ListUsers.Find(item => item.UserName == UserNew.UserName);
            if (sta != null)
            {
                ListUsers.Remove(sta);
            }
            else
                throw new UserException("Does not exist in the system");
            XMLTools.SaveListToXMLSerializer(ListUsers, userPath);
            return true;
        }

        public IEnumerable<User> GetAllUser()
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);
            return from user in ListUsers
                   select user; //no need to Clone()
        }



        public User GetOneUser(string UsserName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(userPath);
            DO.User user = ListUsers.Find(p => p.UserName == UsserName);
            if (user != null)
                return user; //no need to Clone()
            else
                throw new DO.UserException("The UserName number " + UsserName + " not found");
        }

        #endregion User

        #region AdjacentStation
        public bool addAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationPath);

            XElement pair1 = (from p in pairsRootElem.Elements()
                              where p.Element("Station1").Value == AdjacentStationNew.Station1.ToString() && p.Element("Station2").Value == AdjacentStationNew.Station2.ToString() || p.Element("Station2").Value == AdjacentStationNew.Station1.ToString() && p.Element("Station1").Value == AdjacentStationNew.Station2.ToString()
                              select p).FirstOrDefault();

            if (pair1 != null)
                throw new AdjacentStationException("The pair of stations already exists");

            XElement pairElem = new XElement("PairConsecutiveStationsDAO",
                                   new XElement("Station1", AdjacentStationNew.Station1),
                                   new XElement("Station2", AdjacentStationNew.Station2),
                                   new XElement("Distance", AdjacentStationNew.Distance),
                                   new XElement("Time", AdjacentStationNew.Time.ToString()));

            pairsRootElem.Add(pairElem);

            XMLTools.SaveListToXMLElement(pairsRootElem, AdjacentStationPath);
            return true;

        }

        public bool updatingAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationPath);

            XElement pair1 = (from p in pairsRootElem.Elements()
                              where p.Element("Station1").Value == AdjacentStationNew.Station1.ToString() && p.Element("Station2").Value == AdjacentStationNew.Station2.ToString() || p.Element("Station2").Value == AdjacentStationNew.Station1.ToString() && p.Element("Station1").Value == AdjacentStationNew.Station2.ToString()
                              select p).FirstOrDefault();

            if (pair1 != null)
            {

                pair1.Element("Station1").Value = AdjacentStationNew.Station1.ToString();
                pair1.Element("Station2").Value = AdjacentStationNew.Station2.ToString();
                pair1.Element("Distance").Value = AdjacentStationNew.Distance.ToString();
                pair1.Element("Time").Value = AdjacentStationNew.Time.ToString();

                XMLTools.SaveListToXMLElement(pairsRootElem, AdjacentStationPath);
                return true;
            }
            else
                throw new DO.AdjacentStationException("The pair of stations does not exist in the system");
        }
        public bool deleteAdjacentStation(AdjacentStation AdjacentStationNew)
        {
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationPath);

            XElement pair1 = (from p in pairsRootElem.Elements()
                              where p.Element("Station1").Value == AdjacentStationNew.Station1.ToString() && p.Element("Station2").Value == AdjacentStationNew.Station2.ToString() || p.Element("Station2").Value == AdjacentStationNew.Station1.ToString() && p.Element("Station1").Value == AdjacentStationNew.Station2.ToString()
                              select p).FirstOrDefault();

            if (pair1 != null)
            {
                pair1.Remove();
                XMLTools.SaveListToXMLElement(pairsRootElem, AdjacentStationPath);
                return true;
            }
            else
                throw new AdjacentStationException("Does not exist in the system");
        }

        public IEnumerable<AdjacentStation> GetAllAdjacentStation()
        {
            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationPath);

            return (from pair in pairsRootElem.Elements()
                    select new AdjacentStation()
                    {
                        Station1 = Int32.Parse(pair.Element("Station1").Value),
                        Station2 = Int32.Parse(pair.Element("Station2").Value),
                        Distance = Double.Parse(pair.Element("Distance").Value),
                        Time = TimeSpan.Parse(pair.Element("Time").Value),
                    }
                   );
        }




        public AdjacentStation GetOneAdjacentStation(int Station1, int Station2)
        {

            XElement pairsRootElem = XMLTools.LoadListFromXMLElement(AdjacentStationPath);

            AdjacentStation p = (from pair in pairsRootElem.Elements()
                                 where pair.Element("Station1").Value == Station1.ToString() && pair.Element("Station2").Value == Station2.ToString() || pair.Element("Station1").Value == Station2.ToString() && pair.Element("Station2").Value == Station2.ToString()
                                 select new AdjacentStation()
                                 {
                                     Station1 = Int32.Parse(pair.Element("Station1").Value),
                                     Station2 = Int32.Parse(pair.Element("Station2").Value),
                                     Distance = Double.Parse(pair.Element("Distance").Value),
                                     Time = TimeSpan.Parse(pair.Element("Time").Value),
                                 }
                                            ).FirstOrDefault();
            if (p == null)
                return null;
            return p;
        }
        #endregion AdjacentStation

    }
}
//public bool updatingBus(Bus busNew)
//{

//    List<Bus> ListBus = XMLTools.LoadListFromXMLSerializer<Bus>(busPath);
//    Bus bus1 = ListBus.Find(p => p.LicenseNum == busNew.LicenseNum);
//    if (bus1 != null)
//    {

//        Bus current = GetOneBus(busNew.LicenseNum);//שמירה על הערכים הקודמים של הקו
//        busNew.LicenseNum = current.LicenseNum;
//        busNew.Fromdate = current.Fromdate;
//        busNew.TotalTrip = current.TotalTrip;
//        busNew.FuelRemain = current.FuelRemain;
//        busNew.Status = current.Status;
//        ListBus.Remove(bus1);
//        ListBus.Add(busNew); //no nee to Clone()
//    }

//    else
//        throw new DO.BusException("The Identify-Number ");
//    XMLTools.SaveListToXMLSerializer(ListBus, busPath);
//    return true;
//}