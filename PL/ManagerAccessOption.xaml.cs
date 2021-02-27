using BL.BLAPI;
using BO;
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
    /// בדיקת גישה לחלון מנהל
    /// </summary>
    public partial class ManagerAccessOption : Window
    {
         IBL bl = BLFactory.GetBL("1");
        private User newItem = new User();
        public User newItem1 { get => newItem; set => newItem = value; }
        public ManagerAccessOption()
        {
            InitializeComponent();
            DataContext = newItem;

        }

        // אחרי מילוי הפרטים בודק על רשימת המשתשמים אם הפרטים נכונים
        private void Button_ClickLogIn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bl.existingUser(newItem.UserName, Password.Password))//אם קיים במערכת משתמש כזה
                {
                    managerWindow managerWindow = new managerWindow(bl);
                    managerWindow.ShowDialog();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "ERORR", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

    }
}

