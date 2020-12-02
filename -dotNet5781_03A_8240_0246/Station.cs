﻿using System;
using System.Collections.Generic;

namespace _dotNet5781_03A_8240_0246
{
    /// <summary>
    /// Staion for bus
    /// </summary>
    public class Station 
    {
        private const int MAXDIGITS = 1000000;
        private const int MIN_LAT = -90;
        private const int MAX_LAT = 90;
        private const int MIN_LON = -180;
        private const int MAX_LON = 180;

        private static List<int> serials = new List<int>();

        private int busStationKey;
        private double latitude;
        private double longitude;

        /// <summary>
        /// key value should  be unique and max 6 digits
        /// </summary>
        public int BusStationKey
        {
            get { return busStationKey; }

            set
            {
                //if (serials.Contains(value))//key number exists
                //{
                //    throw new ArgumentException(
                //        String.Format("{0} key number exists allready", value));
                //}
                if (value <= 0 && value >= MAXDIGITS)//not a valid key number
                {
                    throw new ArgumentException(
                       String.Format("{0} is not a valid key number", value));
                }
                busStationKey = value;
                serials.Add(BusStationKey);
            }
        }

        public double Latitude
        {
            get { return latitude; }
            set
            {
                if (value >= MIN_LAT && value <= MAX_LAT)
                {
                    latitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Latitude",
                        String.Format("{0} should be between {1} and {2}", value, MIN_LAT, MAX_LAT));
                }
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set
            {
                if (value >= MIN_LON && value <= MAX_LON)
                {
                    longitude = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Longitude",
                        String.Format("{0} should be between {1} and {2}", value, MIN_LON, MAX_LON));
                }
            }

        }

        public String Address { get; set; }

        public override string ToString()
        {
            String result = "BusStation Code: " + busStationKey;
            result += String.Format(", {0}°{1} {2}°{3}",
                Math.Abs(Latitude), (Latitude > 0) ? "N" : "S",
                Math.Abs(Longitude), (Longitude > 0) ? "E" : "W");
            return result;
        }
    }

}

