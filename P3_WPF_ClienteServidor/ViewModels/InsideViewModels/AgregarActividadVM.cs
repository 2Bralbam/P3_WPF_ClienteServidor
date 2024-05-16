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
    public class AgregarActividadVM:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public ICommand AgregarActividadCommand { get; set; }
        private DataService dataService = new DataService();
        public AgregarActividadVM()
        {
            AgregarActividadCommand = new RelayCommand(async()=> { await AgregarActividad(); });
        }

        private async Task AgregarActividad()
        {
            CrearActividadModel actividad = new CrearActividadModel()
            {
                titulo = Titulo,
                descripcion = Descripcion,
                fechaRealizacion = DateTime.Now.ToString("yyyy-MM-dd")
            };
            await dataService.GuardarActividad(actividad);
            Titulo = "";
            Descripcion = "";
            OnPropertyChanged(nameof(Titulo));
            OnPropertyChanged(nameof(Descripcion));
            VMMessaging.HideAgregarActView();
            ActividadModel actividadModel = new ActividadModel()
            {
                Titulo = actividad.titulo,
                Descripcion = actividad.descripcion,
                FechaRealizacion = actividad.fechaRealizacion
            };
            VMMessaging.AgregarActividad(actividadModel);
        }

        
    }
}
