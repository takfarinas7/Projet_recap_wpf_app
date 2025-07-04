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

namespace IdeaManager.UI.Views
{
    /// <summary>
    /// Logique d'interaction pour DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Page
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void NavigateToIdeaForm_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new IdeaFormView());
        }



        private void NavigateToIdeaList_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new IdeaListView());
        }
    }
}