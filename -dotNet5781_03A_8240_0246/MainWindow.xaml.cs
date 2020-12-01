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

namespace _dotNet5781_03A_8240_0246
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BusCompany collection_line;
        private BusLine currentDisplayBusLine;
        public MainWindow()
        {
            InitializeComponent();
            collection_line = new BusCompany();
            //initComboBox();
            collection_line.constractor();
            cbBusLines.ItemsSource = collection_line;
            cbBusLines.DisplayMemberPath = "Number";
            cbBusLines.SelectedIndex = 0;
        }

        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).Number);
        }

        private void ShowBusLine(int busLineNum)
        {
            currentDisplayBusLine = collection_line[busLineNum];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.BusStations;
        }
    }
}
