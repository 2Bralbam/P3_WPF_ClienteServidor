using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P3_WPF_ClienteServidor.ViewModels
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private readonly string[] ValidViews = { "DirectorGeneralView", "JefesView", "SubdirectoresView", "DirectoresView", "MiembrosView", "LoginView" };
        public string NombreVista { get; set; } = "LoginView";
        public MainViewModel()
        {

            VMMessaging.CambiarDeVista += ChangeView;
        }

        private void ChangeView(object? sender, string e)
        {
            if(string.IsNullOrEmpty(e))
            {
#if DEBUG
                MessageBox.Show("La vista especificada no existe");
#endif
                return;
            }
            if(ValidViews.Contains(e))
            {
                NombreVista = e;
            }
            else
            {
#if DEBUG
                MessageBox.Show("La vista especificada no existe");
#endif
            }
            OnPropertyChanged(nameof(NombreVista));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
