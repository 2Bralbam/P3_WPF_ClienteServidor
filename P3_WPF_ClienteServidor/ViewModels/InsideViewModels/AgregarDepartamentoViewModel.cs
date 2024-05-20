using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using P3_WPF_ClienteServidor.Services.AuthServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class AgregarDepartamentoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public DirectoresModel Departamento { get; set; } = new DirectoresModel();
        public List<DirectoresModel> DepartamentosList { get; set; } = new List<DirectoresModel>();
        public DirectoresModel SuperiorSelected { get; set;} = new DirectoresModel();
        public ICommand ConfirmarAgregarDepartamentoCommand { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DataService dataService = new DataService();
        public AgregarDepartamentoViewModel()
        {
            ConfirmarAgregarDepartamentoCommand = new RelayCommand(AgregarDepartamento);
            VMMessaging.HideAgregarDepartamentoEvent += VMMessaging_HideAgregarDepartamentoEvent;
            VMMessaging.AgregandoDepartamentoEvent += VMMessaging_AgregarDepartamentoEvent;
        }

        private void VMMessaging_HideAgregarDepartamentoEvent(object? sender, EventArgs e)
        {
            Departamento = new DirectoresModel();
            OnPropertyChanged(nameof(Departamento));
        }

        private void VMMessaging_AgregarDepartamentoEvent(object? sender, EventArgs e)
        {
            try
            {
                DepartamentosList=new List<DirectoresModel>(VMMessaging.ListaDeDepartamentos.OrderBy(x=>x.Id));
                OnPropertyChanged(nameof(DepartamentosList));
            }
            catch
            {

            }
        }

        private void AgregarDepartamento()
        {
            string Errors = string.Empty;
            try
            {
                if(SuperiorSelected.Id== null)
                {
                    Errors += "Seleccione un superior\n";
                }
                if(string.IsNullOrEmpty(Departamento.Nombre))
                {
                    Errors += "El nombre no puede estar vacío\n";
                }
                if(string.IsNullOrEmpty(Departamento.Password))
                {
                    Errors += "La descripción no puede estar vacía\n";
                }
                if (string.IsNullOrEmpty(Departamento.Username))
                {
                    //USERNAME ES EL MAIL
                    Errors += "El email no puede estar vacío\n";
                }
                else {
                    //PETICION

                    Departamento.IdSuperior = int.Parse(SuperiorSelected.Id);
                    dataService.AgregarDepartamento(Departamento);
                    VMMessaging.AgregarDepartamento(Departamento);
                    VMMessaging.HideAgregarDepartamento();
                    Departamento = new DirectoresModel();
                }
            }
            catch
            {

            }
        }
    }
}
