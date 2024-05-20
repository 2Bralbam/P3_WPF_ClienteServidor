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
    public class EliminarViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; } = "ACT O USER";
        public int IdObject { get; set; } = 0;
        public ActividadModel Actividad { get; set; } = new ActividadModel();
        public string ActivdadDepartamento = "";
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand ConfirmarEliminacion { get; set; }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private DataService _dataService = new DataService();
        public EliminarViewModel()
        {
            ConfirmarEliminacion = new RelayCommand(ConfirmarEliminacionMethod);
            VMMessaging.ShowElimination += ShowElimination;
            VMMessaging.ShowEliminarActividadEvent += VMMessaging_ShowEliminarActividadEvent;


        }

        private void ConfirmarEliminacionMethod()
        {
            try
            {
                if (ActivdadDepartamento == "Departamento")
                {
                    //_dataService.EliminarDepartamento(IdObject);
                    
                    DirectoresModel departamento = new DirectoresModel() { Id = IdObject.ToString() };
                    if(departamento.Id != null)
                    {
                        _dataService.EliminarDepartamento(int.Parse(departamento.Id));

                    }
                    VMMessaging.EliminarDepartamento(departamento);
                    VMMessaging.ExitElim();
                }
                else
                {
                    _ = _dataService.EliminarActividadById(Actividad.Id);
                    VMMessaging.EliminarActividadById(Actividad.Id);
                    VMMessaging.HideEliminarActividad();
                    VMMessaging.HideActividadDetallesView();
                }
            }
            catch
            {

            }
        }

        private void ShowElimination(object? sender, EliminarModel e)
        {
            ActivdadDepartamento = "Departamento";
            Title = e.EliminarTituloString;
            IdObject = e.IdObject;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(IdObject));
        }



        private void VMMessaging_ShowEliminarActividadEvent(object? sender, ActividadModel e)
        {
            ActivdadDepartamento = "Actividad";
            Actividad = e;
            Title = Actividad.Titulo;
            IdObject = Actividad.Id;
            OnPropertyChanged(nameof(Title));
        }
    }

}
