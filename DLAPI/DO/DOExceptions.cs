using System;
using System.Runtime.Serialization;

namespace DO
{
    [Serializable]
    public class BusException : Exception
    {
        public int LicenseNum;
        public BusException(int LicenseNumN) : base() => LicenseNum = LicenseNumN;
        public BusException(int LicenseNumN, string message) :
            base(message) => LicenseNum = LicenseNumN;
        public BusException(int LicenseNumN, string message, Exception innerException) :
            base(message, innerException) => LicenseNum = LicenseNumN;
        public BusException(string message) : base(message) { }
        public override string ToString() => base.ToString() + $",  License number: {LicenseNum}";
    }

    public class LineException : Exception
    {
        public int ID;
        public LineException(int id) : base() => ID = id;

        public LineException(string message) : base(message) { }

        public LineException(int id, string message) :
            base(message) => ID = id;
        public LineException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;

        public override string ToString() => base.ToString() + $",Line ID: {ID}";

    }
    public class LineStationDException : Exception
    {
        public int LineNumber;
        public int stationID;
        public LineStationDException(int LineNumberN, int stationIDN) : base() { LineNumber = LineNumberN; stationID = stationIDN; }
        public LineStationDException(int LineNumberN, int stationIDN ,string message) :
            base(message)
        { LineNumber = LineNumberN; stationID = stationIDN; }
        public LineStationDException(int LineNumberN, int stationIDN, string message ,Exception innerException) :
            base(message, innerException)
        { LineNumber = LineNumberN; stationID = stationIDN; }
        public LineStationDException(string message) : base(message) { }

        public override string ToString() => base.ToString() + $", License number {LineNumber} and Station Id: {stationID}";
    }

    public class StationException : Exception
    {
        public int Code;
        public StationException(int CodeN) : base() => Code = CodeN;
        public StationException(int CodeN, string message) :
            base(message) => Code = CodeN;
        public StationException(int CodeN, string message, Exception innerException) :
            base(message, innerException) => Code = CodeN;
        public StationException(string message) : base(message) { }
        public override string ToString() => base.ToString() + $", Station  number {Code}";

    }
    public class UserException : Exception
    {
        public string UserName;
        public UserException(string UserNameN) : base() => UserName = UserNameN;
        public UserException(string UserNameN, string message) :
            base(message) => UserName = UserNameN;
        public UserException(string UserNameN, string message, Exception innerException) :
            base(message, innerException) => UserName = UserNameN;
        
        public override string ToString() => base.ToString() + $", user Name : {UserName}";
    }
    public class AdjacentStationException : Exception
    {
        public int Station1;
        public int Station2;

        public AdjacentStationException(string message) : base(message) { }
        public AdjacentStationException(int S1, int S2) : base() { Station1 = S1; Station2 = S2; }
        public AdjacentStationException(int S1, int S2, string message) : base(message)
        { Station1 = S1; Station2 = S2; }
        public AdjacentStationException(int S1, int S2, string message, Exception innerException) :
            base(message, innerException)
        { Station1 = S1; Station2 = S2; }

        public override string ToString() => base.ToString() + $",the staions {Station1} and {Station2} no AdjacentStation";
    }
    public class XMLFileLoadCreateException : Exception
    {
        public string xmlFilePath;
        public XMLFileLoadCreateException(string xmlPath) : base() { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message) :
            base(message)
        { xmlFilePath = xmlPath; }
        public XMLFileLoadCreateException(string xmlPath, string message, Exception innerException) :
            base(message, innerException)
        { xmlFilePath = xmlPath; }


            public override string ToString() => base.ToString() + $", fail to load or create xml file: {xmlFilePath}";
        }
    public class BusOnTripException : Exception
    {
        public BusOnTripException()
        {
        }

        public BusOnTripException(string message) : base(message)
        {
        }

        public BusOnTripException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BusOnTripException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
