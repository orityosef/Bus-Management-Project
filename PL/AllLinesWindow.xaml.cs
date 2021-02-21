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
    /// Interaction logic for AllLinesWindow.xaml
    /// </summary>
    public partial class AllLinesWindow : Window
    {
        //  public ObservableCollection<Bus> Buss = new ObservableCollection<Bus>();
         IBL bl = BLFactory.GetBL("1");

        public AllLinesWindow()
        {
            InitializeComponent();
            AllLine.ItemsSource = bl.GetAllBusesLine();
            //  AllBuses.ItemsSource = Buss; //Displays buses on screen
        }
        private void Button_ClickAddLine(object sender, RoutedEventArgs e)
        {
            try
            {
                addLine addLineWindow = new addLine();
                addLineWindow.ShowDialog();
                AllLine.ItemsSource = bl.GetAllBusesLine();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
            InitializeComponent();
            AllLine.ItemsSource = bl.GetAllBusesLine();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var fxElt = sender as FrameworkElement;
            BO.Line CurrentLine = fxElt.DataContext as BO.Line;
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", " DELETE", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    bl.deleteLine(CurrentLine);
                    AllLine.ItemsSource = bl.GetAllBuses();
                    MessageBox.Show("Done", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
            private void AllLine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
