using BL.BLAPI;
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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for AllStationsWindow.xaml
    /// </summary>
    public partial class AllStationsWindow : Window
    {

        readonly IBL bl = BLFactory.GetBL("1");

        public AllStationsWindow()
        {
            InitializeComponent();
            AllStations.ItemsSource = bl.GetAllStation();

        }
        private void Button_ClickAddStation(object sender, RoutedEventArgs e)
        {
            addStation addStationWindow = new addStation();
            addStationWindow.ShowDialog();
            bool a = false;
            try
            {
                if (addStationWindow.ifDone)
                { a = bl.addStation(addStationWindow.newItem1); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            InitializeComponent();
            AllStations.ItemsSource = bl.GetAllStation();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BO.Station Currentstation = fxElt.DataContext as BO.Station;
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", " DELETE", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.deleteStation(Currentstation);
                    AllStations.ItemsSource = bl.GetAllStation();
                    MessageBox.Show("Done", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}

