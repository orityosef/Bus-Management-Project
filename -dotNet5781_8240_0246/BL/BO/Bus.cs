using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;
namespace BO
{
    public class Bus: INotifyPropertyChanged
    {
        private Status status;
        private double fuelRemain;
        public int LicenseNum { get; set; }
        public string LicenseN
        {
            get
            {
                string firstpart, middlepart, endpart;
                string formattedLicense;
                string license = LicenseNum.ToString();
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
            set
            {
                LicenseN = value;
            }
        } 
        public DateTime Fromdate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain
        {
            get => fuelRemain;
            set
            {
                fuelRemain = value;
                OnPropertyChanged("Fuel");
            }
        }
        public int Refuel { get; set; }
        public Status Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged("Status");
            }
        }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
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

    }
}
