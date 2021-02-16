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
using BO;
using BL.BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for addLine.xaml
    /// </summary>
    public partial class addLine : Window
    {

        IBL bl = BLFactory.GetBL("1");
        private BO.Line newItem = new BO.Line();
   
        public BO.Line newItem1 { get => newItem; set => newItem = value; }
        private List<string> areas = new List<string>();
        public addLine()
        {
            InitializeComponent();
            areas.Add("GENERAL");
            areas.Add("NORTH");
            areas.Add("SOUTH");
            areas.Add("CENTER");
            areas.Add("JERUSALEM");
            DataContext = newItem;
            areaCB.ItemsSource = areas;
            station1CB.ItemsSource = bl.GetAllMiniStations();
            station2CB.ItemsSource = bl.GetAllMiniStations();
        }
        private void areaCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (areaCB.SelectedItem.ToString() == "GENERAL")
            { 
                newItem.Aera = (Areas)1;
            }
            if (areaCB.SelectedItem.ToString() == "NORTH")
            {
                newItem.Aera = (Areas)2;
            }
            if (areaCB.SelectedItem.ToString() == "SOUTH")
            {
                newItem.Aera = (Areas)3;
            }
            if (areaCB.SelectedItem.ToString() == "CENTER")
            {
                newItem.Aera = (Areas)4;
            }
            if (areaCB.SelectedItem.ToString() == "JERUSALEM")
            {
                newItem.Aera = (Areas)4;
            }
            
        }
        private void station1CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            newItem.FirstStation = (station1CB.SelectedItem as MiniStation).CodeStation;
        }
        private void station2CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as MiniStation).CodeStation == (station1CB.SelectedItem as MiniStation).CodeStation)
                    throw new Exception("לא ניתן לבחור את אותה תחנה פעמיים");
                newItem.LastStation = (station2CB.SelectedItem as MiniStation).CodeStation;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }

        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((station2CB.SelectedItem as MiniStation).CodeStation == (station1CB.SelectedItem as MiniStation).CodeStation)
                    throw new Exception("לא ניתן לבחור את אותה תחנה פעמיים");
               Station newStation1 = bl.GetOneStation(newItem.FirstStation);
                Station newStation2 = bl.GetOneStation(newItem.LastStation);
                ////AdjacentStation pair = bl.GetOneAdjacentStation(newItem.FirstStation, newItem.LastStation);
                //if (pair == null)//אין עדיין מידע עבור זוג התחנות הללו
                //{
                //    throw new Exception("אין עדיין מידע עבור זוג התחנות הללו");
                //}
                bl.addLine(newItem);
                MessageBox.Show("!בוצע בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (StationException ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); }
            catch (BusException ex) { MessageBox.Show(ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error); Close(); }

        }
        private void TextBox_OnlyNumbers_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //e.Key == Key.Enter ||
            //allow get out of the text box
            if (e.Key == Key.Return || e.Key == Key.Tab)
            {
                Close();
                e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
                return;
            }
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
             || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right)
                return;

            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);

            //allow control system keys
            if (Char.IsControl(c)) return;

            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox

            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other controls
            return;
        }
    }
}
