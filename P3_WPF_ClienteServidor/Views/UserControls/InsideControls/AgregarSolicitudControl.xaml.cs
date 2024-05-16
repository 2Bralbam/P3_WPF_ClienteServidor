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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P3_WPF_ClienteServidor.Views.UserControls.InsideControls
{
    /// <summary>
    /// Lógica de interacción para AgregarSolicitudControl.xaml
    /// </summary>
    public partial class AgregarSolicitudControl : UserControl
    {
        public AgregarSolicitudControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VMMessaging.HideAgregarActView();
        }
    }
}
