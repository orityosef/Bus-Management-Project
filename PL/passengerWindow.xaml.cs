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
    /// Interaction logic for passengerWindow.xaml
    /// </summary>
    public partial class passengerWindow : Window
    {
        IBL bl = BLFactory.GetBL("1");
        private int codeStation1;
        private int codeStation2;
        public passengerWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            lastStationCB.ItemsSource = bl.GetAllMiniStations();
            firstStationCB.ItemsSource = bl.GetAllMiniStations();
        }
        private void firstStationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                codeStation1 = (firstStationCB.SelectedItem as MiniStation).CodeStation;
                dgWays.ItemsSource = bl.GetRelevantWays(codeStation1, codeStation2);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
        private void lastStationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                codeStation2 = (lastStationCB.SelectedItem as MiniStation).CodeStation;
                dgWays.ItemsSource = bl.GetRelevantWays(codeStation1, codeStation2);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
    }
}
