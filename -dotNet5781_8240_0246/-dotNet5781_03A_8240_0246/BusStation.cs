using System;

namespace _dotNet5781_03A_8240_0246
{

    public class BusStation : Station
    {
        static public Random r = new Random();
        /// <summary>
        /// distance from previous BusStation
        /// </summary>
        public double Distance(Station x, Station y)
        {
            /// <summary>
            /// Travel time from previous BusStation
            /// </summary>

            double distance, z, p;
            z = x.Latitude - y.Latitude;
            z = Math.Pow(z, 2);
            p = x.Longitude - y.Longitude;
            p = Math.Pow(p, 2);
            distance = z + p;
            distance = Math.Sqrt(distance);
            return distance;
        }

        public TimeSpan TravelTime
        {
            get
            {
                /// <summary>
                /// Selects a random travel time between one minute and 11 hours.
                /// </summary>
                TimeSpan start = TimeSpan.FromHours(0.03);
                TimeSpan end = TimeSpan.FromHours(11);
                int maxMinutes = (int)((end - start).TotalMinutes);
                int minutes = r.Next(maxMinutes);
                TimeSpan t = start.Add(TimeSpan.FromMinutes(minutes));

                return t;
            }
        }

        public override string ToString()
        {

           
            return base.ToString();
        }
    }
}
