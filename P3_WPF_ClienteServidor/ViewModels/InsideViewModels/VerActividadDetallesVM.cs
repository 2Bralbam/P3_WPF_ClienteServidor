using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class VerActividadDetallesVM : INotifyPropertyChanged
    {
        public ActividadModel Actividad { get; set; } = new();
        public string Eliminando { get; set; } = "False";
        public string Editando { get; set; } = "False";
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public VerActividadDetallesVM()
        {
            VMMessaging.ShowActividadEvent += ShowActividad;
            VMMessaging.ShowEditarActividadEvent += ShowEditarActividad;
            VMMessaging.ShowEliminarActividadEvent += ShowEliminarActividad;
            VMMessaging.HideEliminarActividadEvent += HideEliminarActividad;
            VMMessaging.HideEditarActividadEvent += HideEditarActividad;
        }

        private void HideEditarActividad(object? sender, EventArgs e)
        {
            
            Editando = "False";
            Eliminando = "False";
            OnPropertyChanged(nameof(Editando));
            OnPropertyChanged(nameof(Eliminando));
        }

        private void HideEliminarActividad(object? sender, EventArgs e)
        {
            Eliminando = "False";
            Editando = "False";
            OnPropertyChanged(nameof(Eliminando));
            OnPropertyChanged(nameof(Editando));
        }

        private void ShowEliminarActividad(object? sender, ActividadModel e)
        {
            Actividad=e;
            Eliminando = "True";
            Editando = "False";
            OnPropertyChanged(nameof(Eliminando));
            OnPropertyChanged(nameof(Editando));
        }

        private void ShowEditarActividad(object? sender, ActividadModel e)
        {
           Actividad=e;
            Editando = "True";
            Eliminando = "False";
            OnPropertyChanged(nameof(Editando));
            OnPropertyChanged(nameof(Eliminando));
        }

        private void ShowActividad(object? sender, ActividadModel e)
        {
            Actividad=e;
            OnPropertyChanged(nameof(Actividad));
        }
    }
}
