﻿using BL.BLAPI;
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
    /// Interaction logic for WindowShowLine.xaml
    /// </summary>
    public partial class WindowShowLine : Window
    {
        IBL bl;
        public WindowShowLine(IBL _bl, BO.Line currentLine)
        {
            InitializeComponent();
            bl = _bl;
            DataContext = currentLine;
            lbStationsInLineOnSystem.DataContext = currentLine.ListOfStations.ToList();
            loozDG.ItemsSource = currentLine.ListOfTrips.ToList();
        }
    }
}
