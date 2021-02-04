using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO; 
namespace BL.BO
{
    [Serializable]
    public class BusException : Exception
        {
            public int LicenseNumB;
            public BusException(string message, Exception innerException) :
                base(message, innerException) => LicenseNumB = ((DO.BusException)innerException).LicenseNum;
            public override string ToString() => base.ToString() + $" Invalid license number: {LicenseNumB}";
        }
    [Serializable]
    public class StationException : Exception
    {
        public int Code;
        public StationException(string message, Exception innerException) :
            base(message, innerException) => Code = ((DO.StationException)innerException).Code;
        public override string ToString() => base.ToString() + $", Invalid code number:: {Code}";
    }
    [Serializable]
    public class LineException : Exception
    {
        public int id;
        public LineException(string message, Exception innerException) :
            base(message, innerException) => id = ((DO.LineException)innerException).ID;
        public override string ToString() => base.ToString() + $", Invalid line id: {id}";
    }
    [Serializable]
    public class UserException : Exception
    {
        public String Name;

        public UserException(string message) : base(message)
        {
        }

        public UserException(string message, Exception innerException) :
            base(message, innerException) => Name = ((DO.UserException)innerException).UserName;
        public override string ToString() => base.ToString() + $", Invalid User Name: {Name}";
    }

    [Serializable]
    public class LineStationBException : Exception
        {
            public int LineNumberB;
            public int stationB;
            public LineStationBException(string message, Exception innerException) :
                base(message, innerException)
            {
            LineNumberB = ((DO.LineException)innerException).ID;
            stationB = ((DO.StationException)innerException).Code;
            }
            public override string ToString() => base.ToString() + $", Invalid line id: {LineNumberB} and  Invalid station ID: {stationB}";
        }
    [Serializable]
    public class AdjacentStationException : Exception
    {
        public int st1;
        public int st2;
        public AdjacentStationException(string message, Exception innerException) :
            base(message, innerException)
        {
            st1 = ((DO.AdjacentStationException)innerException).Station1;
            st2 = ((DO.AdjacentStationException)innerException).Station2;
        }
        public override string ToString() => base.ToString() + $", Invalid Station1: {st1} and  Invalid station2: {st2}";
    }

}
