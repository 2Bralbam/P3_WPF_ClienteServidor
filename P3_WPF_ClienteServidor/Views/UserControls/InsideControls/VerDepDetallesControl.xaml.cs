using P3_WPF_ClienteServidor.Models;
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
    /// Lógica de interacción para VerDepDetallesControl.xaml
    /// </summary>
    public partial class VerDepDetallesControl : UserControl
    {
        public VerDepDetallesControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            VMMessaging.ShowEdit(VMMessaging.SelectedUser);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EliminarModel m = new()
            {
                IdObject = int.Parse(this.IdObject.Content.ToString()),
                EliminarTituloString = this.TitleObject.Content.ToString(),
            };
            VMMessaging.ShowElim(m);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            EliminarModel m = new()
            {
                IdObject = int.Parse(this.IdObject.Content.ToString()),
                EliminarTituloString = this.TitleObject.Content.ToString(),
            };
            VMMessaging.ExitEditing();
        }
    }
}
