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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
<<<<<<< HEAD
        readonly IBL bl = BLFactory.GetBL("1");
=======
         IBL bl = BLFactory.GetBL("1");
>>>>>>> aae09601c4729f00cb819a046e483b4a44dda42f
        public MainWindow()
        {
            InitializeComponent();
        }
        private void manager_Click(object sender, RoutedEventArgs e)
        {
            Window ManagerAccessOption = new ManagerAccessOption();
            ManagerAccessOption.Show();
        }

        private void passenger_Click(object sender, RoutedEventArgs e)
        {
            Window passengerWindow = new passengerWindow();
            passengerWindow.Show();
        }
    }
}
