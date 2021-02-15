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
            get => busstations;
          
        }

        /// <summary>
        /// Line number
        /// </summary>
        private int number;
        public int Number {get=>number;set=>number= value;}

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
            if (index == 0)//first one
            {
                AddFirst(busStation);
            }
            else
            {
                if (index > busstations.Count)//overflow
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
        public bool searchStation(BusStation x)//search Station on the bus line
        {
            foreach (BusStation y in busstations)
            {
                if (y.BusStationKey ==x.BusStationKey)
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

        public double DistanceBetween(BusStation x, BusStation y)
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

     
        //public BusLine sub_Route(BusStation one, BusStation two)
        //{
        //    if (!searchStation(one))
        //    {
        //        throw new ArgumentException(
        //                 String.Format("{0} bus not exist", one));
        //    }
        //    if (!searchStation(two))
        //    {
        //        throw new ArgumentException(
        //                 String.Format("{0} bus not exist", two));
        //    }
        //    else
        //    {
        //        BusLine temp = new BusLine();
        //        int first = find_index(one);
        //        int last = find_index(two);
        //        if (first != -1 && last != -1)
        //        {
        //            if (first > last)
        //            {
        //                int help = first;
        //                first = last;
        //                last = help;
        //            }
        //            for (int i = 0; first <= last; i++, first++)
        //            {
        //                temp.BusStations.Insert(i, BusStations[first]);
        //            }
        //            temp.LastStation = temp.BusStations[temp.BusStations.Count - 1];
        //            temp.FirstStation = temp.BusStations[0];
        //        }

        //        else
        //        {
        //            throw new ArgumentException(
        //            String.Format(" bus station not exist"));
        //        }
        //        return temp;
        //    }
        //}

        public int CompareTo(BusLine other)//Compare Between travel time
        {
            double mytotal = totalTime();
            double othertotal = other.totalTime();

            return mytotal.CompareTo(othertotal);
        }
        public int CompareTo(object obj)
        {
            return Time.CompareTo(((BusLine)obj).Time);
        }

        private double totalTime()// total Time travel computer between stations
        {
            double total = 0;
            for (int i = 0; i < busstations.Count - 1; i++)
            {
                total += timeBetween(busstations[i], busstations[i + 1]);
            }

            return total;
        }

        public BusStation SearchStationKey(int x)//cheak if the bus pass this these stations
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
        public bool SearchStationKey1(int x)//bool-cheak if the bus pass this these stations
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
        
        public void remove (BusStation x)//remove station from the busline
        {
            BusStations.Remove(x);
        }
    }
}
