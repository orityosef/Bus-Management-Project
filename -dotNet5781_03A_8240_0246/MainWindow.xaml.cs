
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using _dotNet5781_03A_8240_0246;

namespace _dotNet5781_03A_8240_0246
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusCompany busLines = new BusCompany();
        private BusLine currentDisplayBusLine;
        private static Random rand = new Random();

        public MainWindow()
        {
            createTenbusses();
            InitializeComponent();

            initComboBox();
        }

        private void createTenbusses()
        {
            int k;
            BusLine busline;
            for (int i = 0; i < 10; i++)
            {
                do
                {
                    busline = CreatNewBus();
                    k = busline.Number;

                } while (null != busLines.searchLine(k));

                try
                {
                    busLines.addbus(busline);

                    for (int j = 0; j < 3; j++)
                    {
                        BusStation busstation = creatNewStation();
                        busLines.addStationToLine(k, busstation);
                    }
                }
                catch (ArgumentException ex) { Console.WriteLine(ex); i--; }
            }
        }

        private BusLine CreatNewBus()
        {//create new bus
            BusLine bus = new BusLine();
            bus.Number = rand.Next(0, 1000);
            bus.Zone = (Zone)rand.Next(Enum.GetNames(typeof(Zone)).Length);
            return bus;
        }

        private BusStation creatNewStation()
        {
            {//create new station
                BusStation s = new BusStation();
                s.Latitude = rand.NextDouble() * (33.3 - 31 - 1) + 31;
                s.Longitude = rand.NextDouble() * (35.5 - 34.3 - 1) + 34.3;
                s.BusStationKey = rand.Next(0, 1000000);
                return s;
            }
        }

        private void initComboBox()
        {
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = "Number";
            cbBusLines.SelectedIndex = 0;
            //ShowBusLine(((BusLine)cbBusLines.SelectedItem).Number);
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).Number);
            //ListBox.DataContext = busLines[ComboBox.SelectedIndex].;
        }

        private void ShowBusLine(int index)
        {
            //ListBox.DataContext = busLines[ComboBox.SelectedIndex].;
            currentDisplayBusLine = busLines[index];

            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.BusStations;
        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }

}
