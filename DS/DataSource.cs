﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

using DO;


namespace DS
{
    public static class DataSource
    {

        /// <summary>
        /// DataSource-רשימות של בסיס הנתונים של הפרוייקט
        /// </summary>
        static public Random r = new Random();

        public static List<Bus> listBus;
        public static List<Line> listLine;
        public static List<Station> listStation;
        public static List<LineTrip> listLineTrip;
        public static List<AdjacentStation> listAdjacentStation;
        public static List<BusOnTrip> listBusOnTrip;
        public static List<Trip> listTrip;
        public static List<User> listUser;
        public static List<LineStation> listLineStation;

        //פונקצית אתחול לכל רשימה
        static DataSource()
        {
            InitBuses();
            InitBusLine();
            InitBusStation();
            InitLineStation();
            InitAdjacentStation();
            InitUser();
        }
        public static double Distancebetween(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {
            var Station1 = new GeoCoordinate(Latitude1, Longitude1);
            var Station2 = new GeoCoordinate(Latitude2, Longitude2);
            var distance = Station1.GetDistanceTo(Station2);
            return distance;
        }


        public static void InitBuses()
        {
            listBus = new List<Bus>();

            listBus.Add(new Bus
            {
                LicenseNum = 1234567,
                Fromdate = DateTime.Today.AddYears(-3),
                TotalTrip = 5000,
                FuelRemain = 90,
                Status = Status.Refueling
            });
            listBus.Add(new Bus
            {
                LicenseNum = 6543217,
                Fromdate = DateTime.Today.AddYears(-20),
                TotalTrip = 5000,
                FuelRemain = 1200,
                Status = Status.ReadyToGo
            });
            listBus.Add(new Bus
            {
                LicenseNum = 15823409,
                Fromdate = DateTime.Today.AddYears(-2),
                TotalTrip = 5000,
                FuelRemain = 1200,
                Status = Status.MidRide
            });
            listBus.Add(new Bus
            {
                LicenseNum = 89823409,
                Fromdate = DateTime.Today.AddYears(-1),
                TotalTrip = 5000,
                FuelRemain = 1200,
                Status = Status.ReadyToGo
            });
        }
        public static void InitBusLine()
        {
            listLine = new List<Line>();

            listLine.Add(new Line
            {
                Id = 1,
                LineNumber = 836,
                Area = Areas.GENERAL,
                FirstStation = 234567,
                LastStation = 303999
            });
            listLine.Add(new Line
            {
                Id = 2,
                LineNumber = 450,
                Area = Areas.NORTH,
                FirstStation = 234567,
                LastStation = 987654
            });
            listLine.Add(new Line
            {
                Id = 3,
                LineNumber = 56,
                Area = Areas.SOUTH,
                FirstStation = 246801,
                LastStation = 303988
            });
            listLine.Add(new Line
            {
                Id = 4,
                LineNumber = 480,
                Area = Areas.CENTER,
                FirstStation = 143998,
                LastStation = 303999
            });
        }
        public static void InitBusStation()
        {
            listStation = new List<Station>();

            listStation.Add(new Station
            {
                Code = 143998,
                Name = "Jerusalem",
                Latitude = 31.7888727,
                Longitude = 35.2031491
            });

            listStation.Add(new Station
            {
                Code = 303999,
                Name = "Tel-Aviv",
                Latitude = 32.056151,
                Longitude = 34.7795355

            });

            listStation.Add(new Station
            {
                Code = 303988,
                Name = "Beer-Sheba",
                Latitude = 31.243017,
                Longitude = 34.796741

            });
            listStation.Add(new Station
            {
                Code = 246801,
                Name = "Dimona",
                Latitude = 31.067888,
                Longitude = 35.032528

            });
            listStation.Add(new Station
            {
                Code = 987654,
                Name = "Zefat",
                Latitude = 32.969704,
                Longitude = 35.497204

            });
            listStation.Add(new Station
            {
                Code = 234567,
                Name = "tveria",
                Latitude = 32.787514,
                Longitude = 35.536816

            });
            listStation.Add(new Station
            {
                Code = 965320,
                Name = "Migdal",
                Latitude = 32.839494,
                Longitude = 35.499849

            });
            listStation.Add(new Station
            {
                Code = 852340,
                Name = "Yokneam",
                Latitude = 32.655859,
                Longitude = 35.115152

            });
            listStation.Add(new Station
            {
                Code = 764581,
                Name = "Nevatim",
                Latitude = 31.219761,
                Longitude = 34.882488

            });
            listStation.Add(new Station
            {
                Code = 673029,
                Name = "Latrun Junction",
                Latitude = 31.835052,
                Longitude = 34.978367

            });
        }
        public static void InitLineStation()
        {
            listLineStation = new List<LineStation>();
            //836 קו 
            listLineStation.Add(new LineStation
            {
                LineNumber = 836,
                Station = 852340,//יקנעם
                LineStationIndex = 2,
                PrevStation = 234567,//טבריה
                NextStation = 303999// תל אביב
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 836,
                Station = 234567,//טבריה
                LineStationIndex = 1,
                PrevStation = 0,// תחנה ראשונה
                NextStation = 852340//יקנעם
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 836,
                Station = 303999,// תל אביב
                LineStationIndex = 3,
                PrevStation = 852340,//יקנעם
                NextStation = 0//תחנה אחרונה
            });

            //קו 450
            listLineStation.Add(new LineStation
            {
                LineNumber = 450,
                Station = 965320,//מגדל
                LineStationIndex = 2,
                PrevStation = 234567,//טבריה
                NextStation = 987654//צפת
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 450,
                Station = 234567,//טבריה
                LineStationIndex = 1,
                PrevStation = 0,
                NextStation = 965320//מגדל
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 450,
                Station = 987654,//צפת
                LineStationIndex = 3,
                PrevStation = 965320,//מגדל
                NextStation = 0
            });

            //קו 56
            listLineStation.Add(new LineStation
            {
                LineNumber = 56,
                Station = 764581,//נבטים
                LineStationIndex = 2,
                PrevStation = 246801,//דימונה
                NextStation = 303988//באר שבע
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 56,
                Station = 246801,//דימונה
                LineStationIndex = 1,
                PrevStation = 0,
                NextStation = 764581//נבטים
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 56,
                Station = 303988,//באר שבע
                LineStationIndex = 3,
                PrevStation = 764581,//נבטים
                NextStation = 0
            });

            //קו 480
            listLineStation.Add(new LineStation
            {
                LineNumber = 480,
                Station = 673029,//לטרון
                LineStationIndex = 2,
                PrevStation = 143998,//ירושלים
                NextStation = 303999//תל אביב
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 480,
                Station = 143998,//ירושלים
                LineStationIndex = 1,
                PrevStation = 0,
                NextStation = 673029//לטרון
            });
            listLineStation.Add(new LineStation
            {
                LineNumber = 480,
                Station = 303999,//תל אביב
                LineStationIndex = 3,
                PrevStation = 673029,//לטרון
                NextStation = 0
            });
        }
        public static void InitAdjacentStation()
        {
            listAdjacentStation = new List<AdjacentStation>();

            var distance = Distancebetween(32.787514, 35.536816, 32.655859, 35.115152);
            //טבריה יקנעם
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 234567,
                Station2 = 852340,
                Distance = distance,
                Time = new TimeSpan(0, 58, 0)
            });
            //יקנעם טבריה
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 852340,
                Station2 = 234567,
                Distance = distance,
                Time = new TimeSpan(0, 58, 0)
            });

            distance = Distancebetween(32.655859, 35.115152, 32.056151, 34.7795355);
            //תל אביב יקנעם
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 852340,
                Station2 = 303999,
                Distance = distance,
                Time = new TimeSpan(1, 15, 0)
            });
            //יקנעם תל אביב 
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 303999,
                Station2 = 852340,
                Distance = distance,
                Time = new TimeSpan(1, 15, 0)
            });

            distance = Distancebetween(32.787514, 35.536816, 32.839494, 35.499849);

            //טבריה מגדל
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 234567,
                Station2 = 965320,
                Distance = distance,
                Time = new TimeSpan(0, 15, 0)
            });
            //מגדל טבריה
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 965320,
                Station2 = 234567,
                Distance = distance,
                Time = new TimeSpan(0, 15, 0)
            });
            distance = Distancebetween(32.839494, 35.499849, 32.969704, 35.497204);
            //מגדל צפת
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 965320,
                Station2 = 987654,
                Distance = distance,
                Time = new TimeSpan(0, 42, 0)
            });
            // צפת מגדל
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 987654,
                Station2 = 965320,
                Distance = distance,
                Time = new TimeSpan(0, 42, 0)
            });
            distance = Distancebetween(31.067888, 35.032528, 31.219761, 34.882488);
            //דימונה נבטים
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 246801,
                Station2 = 764581,
                Distance = distance,
                Time = new TimeSpan(0, 34, 0)
            });
            //נבטים דימונה
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 246801,
                Station2 = 764581,
                Distance = distance,
                Time = new TimeSpan(0, 34, 0)
            });
            distance = Distancebetween(31.243017, 34.796741, 31.219761, 34.882488);
            //נבטים באר שבע
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 764581,
                Station2 = 303988,
                Distance = distance,
                Time = new TimeSpan(0, 23, 0)
            });
            //באר שבע נבטים
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 303988,
                Station2 = 764581,
                Distance = distance,
                Time = new TimeSpan(0, 23, 0)
            });
            distance = Distancebetween(31.835052, 34.978367, 31.7888727, 35.2031491);
            //ירושלים לטרון
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 143998,
                Station2 = 673029,
                Distance = distance,
                Time = new TimeSpan(0, 38, 0)
            });
            //לטרון ירושלים
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 673029,
                Station2 = 143998,
                Distance = distance,
                Time = new TimeSpan(0, 38, 0)
            });
            distance = Distancebetween(31.835052, 34.978367, 31.7888727, 35.2031491);
            //לטרון תל אביב
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 673029,
                Station2 = 303999,
                Distance = distance,
                Time = new TimeSpan(1, 7, 0)
            });
            //תל אביב לטרון
            listAdjacentStation.Add(new AdjacentStation
            {
                Station1 = 303999,
                Station2 = 673029,
                Distance = distance,
                Time = new TimeSpan(1, 7, 0)
            });
        }
        public static void InitUser()
        {
           listUser = new List<User>();
            listUser.Add(new User
            {
              UserName="Orit", 
              Password="1234",
              Admit=true 
            });
            listUser.Add(new User
            {
                UserName = "Yosef",
                Password = "6798",
                Admit = true
            });
            listUser.Add(new User
            {
                UserName = "Hila",
                Password = "4321",
                Admit = true
            });
            listUser.Add(new User
            {
                UserName = "Ariel",
                Password = "1008",
                Admit = false
            });

        }
    }
}
