﻿using BL.BLAPI;
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
    /// Interaction logic for addBus.xaml
    /// </summary>
    public partial class addBus : Window
    {

        IBL bl = BLFactory.GetBL("1");
        private Bus newItem = new Bus();
       
        public Bus newItem1 { get => newItem; set => newItem = value; }
        public bool ifDone { get; set; } = false;
        public addBus()
        {
            InitializeComponent();
            DataContext = newItem;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            newItem.Fromdate = dateStart.SelectedDate.Value;
           
            newItem.FuelRemain= 1200;
          
            newItem.Status = Status.ReadyToGo;//מוכן
            ifDone = true;
            Close();
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
