using System;
using System.Collections.Generic;
using System.Collections;

namespace _dotNet5781_03A_8240_0246
{
    public class BusLine : IComparable <BusLine>
    {
        private List<BusStation> busstations = new List<BusStation>();
        public List<BusStation> BusStations
        {
            get
            {
                List<BusStation> temp = new List<BusStation>(busstations);
                return temp;
            }
        }

        //public readonly List<BusStation> busStations;

        public BusLine()
        {
            //busStations = new List<BusStation>();
        }
        /// <summary>
        /// Line number
        /// </summary>
        public int Number { get; set; }
        public double Time
        {
            get { return Time; }

            set { timeBetween(FirstStation,LastStation); }
        }
        public BusStation FirstStation { get; private set; }
        public BusStation LastStation { get; private set; }
        public Zone Zone { get; set; }

        public void AddLast(BusStation busStation)
        {
            busstations.Add(busStation);
            LastStation = busstations[busstations.Count - 1];
        }
        public void AddFirst(BusStation busStation)
        {
            busstations.Insert(0, busStation);
            FirstStation = busstations[0];
        }
        public void Add(int index, BusStation busStation)
        {
            if (index == 0)
            {
                AddFirst(busStation);
            }
            else
            {
                if (index > busstations.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }
                if (index == busstations.Count)
                {
                    busstations.Insert(index, busStation);
                    LastStation = busstations[busstations.Count - 1];
                }
            }
        }


        public bool search(Station x)
        {
            foreach (Station y in busstations)
            {
                if (y == x)
                {
                    return true;
                }
            }
            return false;
        }

        public double timeBetween(BusStation one, BusStation two)
        {
            return one.TravelTime.Subtract(two.TravelTime).TotalMinutes;
        }

        public double DistanceBetween(Station x, Station y)
        {
            double distance, z, p;
            z = x.Latitude - y.Latitude;
            z = Math.Pow(z, 2);
            p = x.Longitude - y.Longitude;
            p = Math.Pow(p, 2);
            distance = z + p;
            distance = Math.Sqrt(distance);
            return distance;
        }

        public int SubRoute(Station x, Station y)
        {
            if (search(x))
            {
                if (search(y))
                {
                    return Number;
                }
            }
            throw new FormatException("The bus does not pass through these stations");

        }

        public int CompareTo(BusLine other)
        {
            double mytotal = totalTime();
            double othertotal = other.totalTime();

            return mytotal.CompareTo(othertotal);
        }

        private double totalTime()
        {
            double total = 0;
            for (int i = 0; i < busstations.Count - 1; i++)
            {
                total += timeBetween(busstations[i], busstations[i + 1]);
            }

            return total;
        }

        public BusStation SearchStationKey(int x)
        {
            foreach (BusStation y in busstations)
            {
                if (y.BusStationKey == x)
                {
                    return y;
                }
            }
            return null;
        }
        public bool SearchStationKey1(int x)
        {
            foreach (Station y in BusStations)
            {
                if (y.BusStationKey == x)
                {
                    return true;
                }
            }
            return false;
        }
        public int CompareTo(object obj)
        {
            return Time.CompareTo(((BusLine)obj).Time);
        }
        public void remove (BusStation x)
        {
            BusStations.Remove(x);
        }
    }
}
