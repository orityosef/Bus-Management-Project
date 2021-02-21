using BL.BLAPI;
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
        //  public ObservableCollection<Bus> Buss = new ObservableCollection<Bus>();
         IBL bl = BLFactory.GetBL("1");

        public AllBusesWindow()
        {
            InitializeComponent();
            AllBuses.DataContext = bl.GetAllBuses().ToString();
           // AllBuses.ItemsSource = bl.GetAllBuses();
            //  AllBuses.ItemsSource = Buss; //Displays buses on screen
        }
        private void Treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                var fxElt = sender as FrameworkElement;
                Bus CurrentBus = fxElt.DataContext as Bus;
                btn.IsEnabled = false;
                bl.Treatment(CurrentBus.LicenseNum.ToString());
                TreatmentT(CurrentBus, 12000, btn);
               
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Button btn = sender as Button;
                btn.IsEnabled = true;
            }
        }
        private void TreatmentT(Bus lineData, int time, Button btn)//Fuel button- Refueling
        {
            btn.IsEnabled = false;
            //           btn.Background = Brushes.Honeydew;

            List<Object> lst = new List<object> { lineData, time, btn };

            BackgroundWorker TreatmentT = new BackgroundWorker();
            TreatmentT.DoWork += TreatmentT_DoWork;
            TreatmentT.RunWorkerCompleted += TreatmentT_RunWorkerCompleted;

            TreatmentT.RunWorkerAsync(lst);
        }

        private void TreatmentT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //Fuel button- Refueling is over 
        {
            List<Object> lst = (List<object>)e.Result;
            Bus currentUser = lst[0] as Bus;
            Button btn = lst[2] as Button;

            currentUser.Status = Status.ReadyToGo;

            btn.IsEnabled = true;
            //btn.Background = Brushes.MintCream;
            //         throw new NotImplementedException();
        }

        private void TreatmentT_DoWork(object sender, DoWorkEventArgs e)//Fuel button- Refueling process
        {
            List<Object> lst = (List<object>)e.Argument;
            Bus currentUser = lst[0] as Bus;
            currentUser.Status = Status.Treatment;

            int value = (int)lst[1];    //3000 time
            Thread.Sleep(value);
            MessageBox.Show("The treatment was performed successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            e.Result = lst;          //btn
        }
        private void Refuelling_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                var fxElt = sender as FrameworkElement;
                Bus CurrentBus = fxElt.DataContext as Bus;
                btn.IsEnabled = false;
                bl.Refuelling(CurrentBus.LicenseNum.ToString());
                tidluk(CurrentBus, 12000, btn);
                CurrentBus.FuelRemain = 1200;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                Button btn = sender as Button;
                btn.IsEnabled = true;
            }
        }
       
        private void Tidluk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //Fuel button- Refueling is over 
        {
            List<Object> lst = (List<object>)e.Result;
            Bus currentUser = lst[0] as Bus;
            Button btn = lst[2] as Button;

            currentUser.Status = Status.ReadyToGo;

            btn.IsEnabled = true;
            //btn.Background = Brushes.MintCream;
            //         throw new NotImplementedException();
        }

        private void Tidluk_DoWork(object sender, DoWorkEventArgs e)//Fuel button- Refueling process
        {
            List<Object> lst = (List<object>)e.Argument;
            Bus currentUser = lst[0] as Bus;
            currentUser.Status = Status.Refueling;

            int value = (int)lst[1];    //3000 time
            Thread.Sleep(value);
            MessageBox.Show("Refueling performed successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
            e.Result = lst;          //btn
        }

        private void tidluk(Bus lineData, int time, Button btn)//Fuel button- Refueling
        {
            btn.IsEnabled = false;
            //           btn.Background = Brushes.Honeydew;

            List<Object> lst = new List<object> { lineData, time, btn };

            BackgroundWorker tidluk = new BackgroundWorker();
            tidluk.DoWork += Tidluk_DoWork;
            tidluk.RunWorkerCompleted += Tidluk_RunWorkerCompleted;

            tidluk.RunWorkerAsync(lst);
        }
        private void Button_ClickAddBus(object sender, RoutedEventArgs e)
        {
            addBus addBusWindow = new addBus();
            addBusWindow.ShowDialog();
            bool a = false;
            try
            {
                if (addBusWindow.ifDone)
                { a = bl.addBus(addBusWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            AllBuses.ItemsSource = bl.GetAllBuses();
            if (a)
            {
                MessageBox.Show("Done", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)//כפתור מחיקה מתוך הרשימה
        {
            var fxElt = sender as FrameworkElement;
            Bus CurrentBus = fxElt.DataContext as Bus;
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", " DELETE", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.deleteBus(CurrentBus);
                    AllBuses.ItemsSource = bl.GetAllBuses();
                    MessageBox.Show("Done", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            
        }
       
    }
}
