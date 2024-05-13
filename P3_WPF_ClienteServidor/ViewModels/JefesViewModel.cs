using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.ViewModels
{
    public class JefesViewModel:INotifyPropertyChanged
    {
        public RelayCommand LogoutCommand { get; set; }
        public string _contenidoView;
        public string ContenidoView
        {
            get
            {
                return _contenidoView;
            }
            set
            {
                _contenidoView = value;
                OnPropertyChanged(nameof(ContenidoView));
            }
        }
        public RelayCommand ActividadesCommand { get; set; }
        public RelayCommand MiembrosCommand { get; set; }
        public RelayCommand JefesCommand { get; set; }
        public RelayCommand SubdirectoresCommand { get; set; }
        public RelayCommand DirectoresCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public JefesViewModel()
        {
            _contenidoView = "AdmActividades";
            LogoutCommand = new RelayCommand(Logout);
            ActividadesCommand = new RelayCommand(Actividades);
            MiembrosCommand = new RelayCommand(Miembros);
            JefesCommand = new RelayCommand(Jefes);
            SubdirectoresCommand = new RelayCommand(Subdirectores);
            DirectoresCommand = new RelayCommand(Directores);
        }

        private void Actividades()
        {
            if (ContenidoView != "AdmActividades")
            {
                ContenidoView = "AdmActividades";
            }
        }

        private void Miembros()
        {
            if (ContenidoView != "AdmMiembros")
            {
                ContenidoView = "AdmMiembros";
            }
        }

        private void Jefes()
        {
            if (ContenidoView != "AdmJefes")
            {
                ContenidoView = "AdmJefes";
            }
        }

        private void Directores()
        {
            if (ContenidoView != "AdmDirectores")
            {
                ContenidoView = "AdmDirectores";
            }
        }

        private void Subdirectores()
        {
            if (ContenidoView != "AdmSubdirectores")
            {
                ContenidoView = "AdmSubdirectores";
            }
        }

        private void Logout()
        {
            VMMessaging.CambiarVista("LoginView");
        }
    }
}
