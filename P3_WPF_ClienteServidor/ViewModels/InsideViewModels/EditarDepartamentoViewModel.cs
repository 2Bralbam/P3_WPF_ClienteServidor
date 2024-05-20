using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using P3_WPF_ClienteServidor.Services.AuthServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class EditarDepartamentoViewModel:INotifyPropertyChanged
    {
        public DirectoresModel EditingDepartamento { get; set; }
        public DirectoresModel DepartamentoSelected { get; set; }
        public ObservableCollection<DirectoresModel> Superiores { get; set; }
        public DirectoresModel SuperiorSelected { get; set; }
        public ICommand ConfirmarEdicionCommand { get; set; }
        public ICommand CancelarEditarCommand { get; set; }
        private DataService DataService = new DataService();
        public EditarDepartamentoViewModel()
        {
            ConfirmarEdicionCommand = new RelayCommand(ConfirmarEdicion);
            CancelarEditarCommand = new RelayCommand(() =>
            {
                VMMessaging.ExitEditV();
            });
            VMMessaging.EditandoDepartamentoEvent += VMMessaging_EditandoDepartamentoEvent;
        }

        private void ConfirmarEdicion()
        {
            string Errores = string.Empty;
            if(EditingDepartamento == null)
            {
                return;
            }
            if(string.IsNullOrEmpty(EditingDepartamento.Nombre))
            {
                Errores += "El nombre no puede estar vacío\n";
            }
            if (string.IsNullOrEmpty(EditingDepartamento.Rol))
            {
                Errores += "El rol no puede estar vacío\n";
            }
            if (string.IsNullOrEmpty(EditingDepartamento.Username))
            {
                Errores += "El username no puede estar vacío\n";
            }
            //SI PASSWORD ES NULL SIGNIFICA QUE PASSWORD NO CAMBIA
            if(Errores!= string.Empty)
            {
                EditingDepartamento.IdSuperior = int.Parse(SuperiorSelected.Id);
                DataService.EditarDepartamento(EditingDepartamento);
            }
            else
            {
                MessageBox.Show(Errores);
            }
            
        }

        private void VMMessaging_EditandoDepartamentoEvent(object? sender, DirectoresModel e)
        {
            if (e != null)
            {
                try
                {
                    DepartamentoSelected = e;
                    EditingDepartamento = new DirectoresModel
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        Rol = e.Rol,
                        Username = e.Username,
                        Password = e.Password,
                    };
                    OnPropertyChanged(nameof(EditingDepartamento));
                    Superiores = new ObservableCollection<DirectoresModel>(VMMessaging.ListaDeDepartamentos.Where(x => x.IdSuperior <= int.Parse(e.Id)).ToList());
                    OnPropertyChanged(nameof(Superiores));
                }
                catch
                {
                    DepartamentoSelected = null;
                    EditingDepartamento = null;
                    Superiores = null;
                }
            }
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
