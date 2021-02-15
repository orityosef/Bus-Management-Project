using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;


namespace _dotNet5781_3B_8240_0246
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        static public Random r = new Random();
        public ObservableCollection<Bus> Buss = new ObservableCollection<Bus>();


        public MainWindow()
        {
            InitializeComponent();
            List <Bus> buses = Add10Buss();
            for (int i = 0; i < 10; ++i)
            {
               
                Buss.Add(buses[i]);
               
            }
            lvBuses.ItemsSource = Buss; //Displays 10 buses on screen
        }
        public List<Bus> Add10Buss() // Adds 10 buses randomly
        {
            List<Bus> buses = new List<Bus>();

            for (int i = 0; i < 10; i++)
            {
                Bus bus = new Bus();
                bus.Fuel= r.Next(0, 1200);
                bus.Km = r.Next(0, 10000);
                bus.State();
                buses.Add(bus);

            }
            buses[0].Checkup = new DateTime(2005, 12, 12);
            buses[0].State();
            buses[1].Km = 19000;
            buses[1].State();
            buses[2].Fuel = 0;
            buses[2].State();
            return buses;
        }
        private void Button_Click(object sender, RoutedEventArgs e) //Fuel button
        {
            Button btn = sender as Button;
            Bus currentuser = btn.DataContext as Bus;

            btn.IsEnabled = false;
            
            tidluk(currentuser, 12000, btn);
        }


        private void Tidluk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) //Fuel button- Refueling is over 
        {
            List<Object> lst = (List<object>)e.Result;
            Bus currentUser = lst[0] as Bus;
            Button btn = lst[2] as Button;

            currentUser.Status = STATE.ReadyToGo;

            btn.IsEnabled = true;
            //btn.Background = Brushes.MintCream;
            //         throw new NotImplementedException();
        }

        private void Tidluk_DoWork(object sender, DoWorkEventArgs e)//Fuel button- Refueling process
        {
            List<Object> lst = (List<object>)e.Argument;
            Bus currentUser = lst[0] as Bus;
         
            currentUser.Refuelling(1200);
            currentUser.Status = STATE.Refueling;

            int value = (int)lst[1];    //3000 time
            Thread.Sleep(value);

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
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void btnDrive_Click(object sender, RoutedEventArgs e) //Travel button opens a new window
        {
            Button btn = sender as Button;
            Bus currentuser = btn.DataContext as Bus;
            SecondWindow secondWindow = new SecondWindow(currentuser);
            //secondWindow.InputChanged += OnDialogInputChanged;
            secondWindow.Show();
    
        }

    

        private void AddBus_Click(object sender, RoutedEventArgs e) //Adds a new bus at random
        {
            Bus bus = new Bus();
            bus.State();
            bus.Fuel = r.Next(0, 1200);
            bus.Km = r.Next(0, 10000);
            Buss.Add(bus);
        }

    }
}
