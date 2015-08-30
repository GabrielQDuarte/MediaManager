﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using MediaManager.Helpers;
using MediaManager.Model;
using MediaManager.ViewModel;

namespace MediaManager.Forms
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class frmRenomear : Window
    {
        public frmRenomear()
        {
            InitializeComponent();
            RenomearViewModel renomearVM = new RenomearViewModel();
            DataContext = renomearVM;
        }
    }
}