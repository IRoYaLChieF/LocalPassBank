﻿using System;
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

namespace LocalPassBank.Views
{
    /// <summary>
    /// Logique d'interaction pour SimpleDialog.xaml
    /// </summary>
    public partial class SimpleDialog : UserControl
    {
        public SimpleDialog(String message)
        {
            InitializeComponent();
            Message.Text = message;
        }
    }
}
