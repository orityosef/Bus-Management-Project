using System;
using System.Collections.Generic;
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

namespace _dotNet5781_3B_8240_0246
{
    /// <summary>
    /// Interaction logic for SecondWindow.xaml
    /// </summary>

    public partial class SecondWindow : Window //Travel button
    {
        private Bus myBus;
        //        public Bus DrivingBus { get => myBus; }
        public SecondWindow(Bus currentBus)
        {
            InitializeComponent();
            myBus = currentBus;
        }


        //Receives a KM from the user and checks that the bus can make the trip
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtBoxKMS.Text== "")
            {
                MessageBox.Show("The number entered is invalid");
            }
            else
            { 
            int distance = int.Parse(this.txtBoxKMS.Text);

                if (myBus.Fuel < distance)
                {
                    MessageBox.Show("There is not enough fuel");
                }
                else if (myBus.Status != STATE.ReadyToGo)
                {
                    MessageBox.Show("The bus is not ready to ride");
                }
                else
                {
                    Button btn = sender as Button;
                    btn.IsEnabled = false;

                    BackgroundWorker gamadkatansheli = new BackgroundWorker();
                    gamadkatansheli.DoWork += Gamadkatansheli_DoWork;
                    gamadkatansheli.WorkerReportsProgress = true;
                    gamadkatansheli.ProgressChanged += Gamadkatansheli_ProgressChanged;
                    gamadkatansheli.RunWorkerCompleted += Gamadkatansheli_RunWorkerCompleted;

                    List<Object> args = new List<object> { distance, btn };
                    gamadkatansheli.RunWorkerAsync(args);
                    
                }
             }
        }
        //The bus makes the trip
        private void Gamadkatansheli_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> args = e.Argument as List<object>;
            int distance = (int)args[0];

            myBus.Fuel -= distance;
            myBus.Status = STATE.MidRide;
            myBus.Km += distance;
            for (int i = 1; i <= 8; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i * 100 / 8);
                Thread.Sleep(distance);
            }

            e.Result = args[1];
       }
        //A ProgressChanged that shows a bus making the trip
        private void Gamadkatansheli_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int percentage = e.ProgressPercentage;
            this.prgBar.Value = percentage;
        }

        //The bus has finished the journey and is ready for the next journey
        private void Gamadkatansheli_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Button btn = e.Result as Button;
            btn.IsEnabled =true;
            myBus.Status = STATE.ReadyToGo;
            prgBar.Value = 0;
        }
    }
}

