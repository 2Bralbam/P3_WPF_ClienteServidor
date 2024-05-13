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

namespace P3_WPF_ClienteServidor.Views.UserControls
{
    /// <summary>
    /// Lógica de interacción para SubdirectoresControl.xaml
    /// </summary>
    public partial class SubdirectoresControl : UserControl
    {
        string SelectedButtonColor = "#86C98A";
        string UnselectedButtonColor = "#689D99";
        Brush SelectedColor { get; set; }
        Brush UnselectedColor { get; set; }
        List<Button> buttons = new();
        public SubdirectoresControl()
        {
            InitializeComponent();
            View.Opacity = 0;
            this.Loaded += MainView_Loaded;
            this.Unloaded += ViewUnloaded;
            BtnActividades.Click += BtnActividades_Click;
            BtnMiembros.Click += BtnMiembros_Click;
            BtnJefes.Click += BtnJefes_Click;
            buttons.Add(BtnActividades);
            buttons.Add(BtnMiembros);
            buttons.Add(BtnJefes);
            BrushConverter bc = new BrushConverter();
            SelectedColor = (Brush)bc.ConvertFrom(SelectedButtonColor);
            UnselectedColor = (Brush)bc.ConvertFrom(UnselectedButtonColor);
            BtnActividades.Background = SelectedColor;
        }
        private async void CheckButtons(object sender)
        {
            if (sender == null)
            {
                return;
            }
            if ((sender as Button).Background == SelectedColor)
            {
                return;
            }
            //foreach (var button in buttons)
            //{
            //    if (button == sender)
            //    {
            //        button.Background = SelectedColor;
            //    }
            //    else
            //    {
            //        button.Background = UnselectedColor;
            //    }
            //}
            Parallel.ForEach(buttons, button =>
            {
                if (button == sender)
                {
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        button.Background = SelectedColor;
                    });

                }
                else
                {
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        button.Background = UnselectedColor;
                    });
                }
            });
        }

        private void BtnJefes_Click(object sender, RoutedEventArgs e)
        {
            CheckButtons(sender);
        }

        private void BtnMiembros_Click(object sender, RoutedEventArgs e)
        {
            CheckButtons(sender);
        }

        private void BtnActividades_Click(object sender, RoutedEventArgs e)
        {
            CheckButtons(sender);
        }
        private void ViewUnloaded(object sender, RoutedEventArgs e)
        {
            View.BeginAnimation(OpacityProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(.5)));
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {

            View.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(.5)));
        }
    }
}
