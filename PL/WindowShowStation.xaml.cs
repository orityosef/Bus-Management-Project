using BL.BLAPI;
using BO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaction logic for WindowShowStation.xaml
    /// </summary>
    public partial class WindowShowStation : Window
    {
        IBL bl = BLFactory.GetBL("1");
        private Stopwatch stopWatch;
        private bool isTimerRun;
        BackgroundWorker timerworker;
        TimeSpan tsStartTime;

        public WindowShowStation(IBL _bl, BO.Station currentStation)
        {
            InitializeComponent();
            bl = _bl;
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
            tsStartTime = DateTime.Now.TimeOfDay;
            stopWatch.Restart();
            isTimerRun = true;
            timerworker.RunWorkerAsync(currentStation);
            DataContext = currentStation;
            try { lbLinesInStationOnSystem.ItemsSource = currentStation.ListOfLines.ToList(); }
            catch { }//תיתפס כאן חריגה במצב שבו אין קווים שעוברים בתחנה
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TimeSpan tsCurrentTime = tsStartTime + stopWatch.Elapsed;
            string timerText = tsCurrentTime.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
            Station currentStation = e.UserState as Station;
            dgForStation.ItemsSource = bl.GetLineTimingsPerStation(currentStation, tsCurrentTime);
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(231, e.Argument);
                Thread.Sleep(1000);
            }
        }

        private void lbLinesInStationOnSystem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
