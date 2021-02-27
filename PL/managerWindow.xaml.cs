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
    ///תפריט בחירת תצוגה 
    /// </summary>
    public partial class managerWindow : Window
    {
         IBL bl = BLFactory.GetBL("1");
        public managerWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }
        private void Buses_Click(object sender, RoutedEventArgs e)
        {
            Window AllBusesWindow = new AllBusesWindow();
            AllBusesWindow.Show();
        }
         private void Lines_Click(object sender, RoutedEventArgs e)
        {
            Window AllLinesWindow = new AllLinesWindow();
            AllLinesWindow.Show();
        }
         private void Stations_Click(object sender, RoutedEventArgs e)
        {
            Window AllStationsWindow = new AllStationsWindow();
            AllStationsWindow.Show();
        }
    }
}
