using BL.BLAPI;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AllBusesWindow.xaml
    /// </summary>
    public partial class AllBusesWindow : Window
    {
        public ObservableCollection<Bus> Buss = new ObservableCollection<Bus>();
        IBL bl;

        public AllBusesWindow(IBL _bl)
        {
            //InitializeComponent();
            //bl = _bl;
            //this.DataContext = bl.GetAllBuses().ToString();
           // AllBuses.DataContext = Buss; //Displays buses on screen
        }
        //        private void Refuelling_Click(object sender, RoutedEventArgs e) //Fuel button
        //        {
        //            Button btn = sender as Button;
        //            Bus currentuser = btn.DataContext as Bus;

        //            btn.IsEnabled = false;

        //            tidluk(currentuser, 12000, btn);
        //        }


        //        private void Tidluk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //Fuel button- Refueling is over 
        //        {
        //            List<Object> lst = (List<object>)e.Result;
        //            Bus currentUser = lst[0] as Bus;
        //            Button btn = lst[2] as Button;

        //            currentUser.Status = STATE.ReadyToGo;

        //            btn.IsEnabled = true;
        //            //btn.Background = Brushes.MintCream;
        //            //         throw new NotImplementedException();
        //        }

        //        private void Tidluk_DoWork(object sender, DoWorkEventArgs e)//Fuel button- Refueling process
        //        {
        //            List<Object> lst = (List<object>)e.Argument;
        //            Bus currentUser = lst[0] as Bus;

        //            currentUser.Refuelling(1200);
        //            currentUser.Status = STATE.Refueling;

        //            int value = (int)lst[1];    //3000 time
        //            Thread.Sleep(value);

        //            e.Result = lst;          //btn
        //        }

        //        private void tidluk(Bus lineData, int time, Button btn)//Fuel button- Refueling
        //        {
        //            btn.IsEnabled = false;
        //            //           btn.Background = Brushes.Honeydew;

        //            List<Object> lst = new List<object> { lineData, time, btn };

        //            BackgroundWorker tidluk = new BackgroundWorker();
        //            tidluk.DoWork += Tidluk_DoWork;
        //            tidluk.RunWorkerCompleted += Tidluk_RunWorkerCompleted;

        //            tidluk.RunWorkerAsync(lst);
        //        }
        //        private void MainWindow_Closing(object sender, CancelEventArgs e)
        //        {
        //            Environment.Exit(Environment.ExitCode);
        //        }

        //        private void btnDrive_Click(object sender, RoutedEventArgs e) //Travel button opens a new window
        //        {
        //            Button btn = sender as Button;
        //            Bus currentuser = btn.DataContext as Bus;
        //            SecondWindow secondWindow = new SecondWindow(currentuser);
        //            //secondWindow.InputChanged += OnDialogInputChanged;
        //            secondWindow.Show();

        //        }



        //        private void AddBus_Click(object sender, RoutedEventArgs e) //Adds a new bus at random
        //        {
        //            Bus bus = new Bus();
        //            bus.State();
        //            bus.Fuel = r.Next(0, 1200);
        //            bus.Km = r.Next(0, 10000);
        //            Buss.Add(bus);
        //        }
    }
}
