﻿using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P3_WPF_ClienteServidor.Views.UserControls.InsideControls
{
    /// <summary>
    /// Lógica de interacción para ActividadesControl.xaml
    /// </summary>
    public partial class ActividadesControl : UserControl
    {
        public ActividadesControl()
        {
            InitializeComponent();
            View.Opacity = 0;
            this.Loaded += MainView_Loaded;
            this.Unloaded += ViewUnloaded;
            ListView.SizeChanged += async(sender,e) => _ = ActividadesControl_SizeChanged(sender, e);
        }

        private async Task ActividadesControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double Size = e.NewSize.Width;
            double NewColumSize = (Size / 4)-2;
            Uno.Width = NewColumSize;
            Dos.Width = NewColumSize;
            Tres.Width = NewColumSize;
            Cuatro.Width = NewColumSize;

        }

        private void ViewUnloaded(object sender, RoutedEventArgs e)
        {
            View.BeginAnimation(OpacityProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.5)));
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {

            View.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(.5)));
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            VMMessaging.ShowActividad((ActividadModel)ListView.SelectedItem);
        }
    }
}
