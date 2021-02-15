using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _dotNet5781_3B_8240_0246
{
    public class Bus : INotifyPropertyChanged
    {

        static public Random r = new Random();

        static public int GlobalKM { get; private set; }
        private const int FULLTANK = 1200;
        private STATE status;
        private int fuel;
        public DateTime StartingDate { get; set; }
        private string license;
        private int km;

        //Allows you to change a list after it is listed

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(String propertyName)
        {
            //if (PropertyChanged != null)
            //{
            //    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //}
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(String.Empty));
        }

        //property enum Bus condition
        public STATE Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        //property Deadtime of treatment
        public DateTime Checkup { get; set; }
        public int Fuel
        {
            get => fuel;
            set
            {
                fuel = value;
                OnPropertyChanged("Fuel");
            }
        }

        //property num of km since the  treatment
        public int Km
        {
            get { return km; }
            set
            {
                if (value >= 0)
                {
                    km = value;
                    OnPropertyChanged("Km");
                }
            }
        }
        //property license number
        public string License
        {
            get
            {

                return ToString();
            }

            private set
            {
                if ((StartingDate.Year < 2018 && value.Length == 7) || (StartingDate.Year >= 2018 && value.Length == 8))
                {
                    license = value;
                    OnPropertyChanged("License");
                }
                else
                {
                    throw new Exception("license not valid");
                }
            }
        }
        //Sets a random start date
        public DateTime RandomDay()
        {
            //pick the hour
            DateTime start = new DateTime(1995, r.Next(1, 13), r.Next(1, 28));
            int range = (DateTime.Today - start).Days;
            return start.AddDays(r.Next(range));
        }
        //ctor
        public Bus()
        {
            StartingDate = RandomDay();
            if (StartingDate.Year < 2018)
            {
                int i = r.Next(1000000, 10000000);
                string int_str = i.ToString();
                License = int_str;
            }
            else
            {
                int i = r.Next(10000000, 100000000);
                string int_str = i.ToString();
                License = int_str;
            }
        }

        //license number according to the year
        public override string ToString()
        {
            string firstpart, middlepart, endpart;
            string formattedLicense;
            if (license.Length == 7)
            {
                // xx-xxx-xx
                firstpart = license.Substring(0, 2);
                middlepart = license.Substring(2, 3);
                endpart = license.Substring(5, 2);
                formattedLicense = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
            }
            else
            {
                // xxx-xx-xxx
                firstpart = license.Substring(0, 3);
                middlepart = license.Substring(3, 2);
                endpart = license.Substring(5, 3);
                formattedLicense = String.Format("{0}-{1}-{2}", firstpart, middlepart, endpart);
            }
            return String.Format("{0,-10}", formattedLicense);
        }

        //Treatment function
        //Sets the date for the day
        public DateTime Maintenance()
        {
            Checkup = DateTime.Today;
            Refuelling(Fuel);
            return Checkup;
        }
        //Treatment function
        public DateTime Maintenance(DateTime checkup)
        {
            Checkup = checkup;
            return Checkup;
        }

        //Refueling function
        public void Refuelling(int fuel)
        {
            Fuel = FULLTANK;
        }

        //Selects the status of the bus according to the data
        // ReadyToGo ,MidRide , Refueling, Treatment
        public STATE State()
        {
            DateTime today = DateTime.Today;
            if (Fuel < 10)
            {
                if ((km >= 20000) || (today.Year > Checkup.Year))
                {
                    status = STATE.Treatment;
                }
                status = STATE.Refueling;
            }
            else
            {
                status = STATE.ReadyToGo;
            }
            return status;
        }
        //status!= STATE.Treatment
    }
}