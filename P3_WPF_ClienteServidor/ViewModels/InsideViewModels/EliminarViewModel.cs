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
    public class EliminarViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; } = "ACT O USER";
        public int IdObject { get; set; } = 0;
        public ActividadModel Actividad { get; set; } = new ActividadModel();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public EliminarViewModel()
        {
            VMMessaging.ShowElimination += ShowElimination;
            VMMessaging.ShowEliminarActividadEvent += VMMessaging_ShowEliminarActividadEvent;


        }
        private void ShowElimination(object? sender, EliminarModel e)
        {
            Title = e.EliminarTituloString;
            IdObject = e.IdObject;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(IdObject));
        }



        private void VMMessaging_ShowEliminarActividadEvent(object? sender, ActividadModel e)
        {
            Actividad = e;
            Title = Actividad.Titulo;
            IdObject = Actividad.Id;
            OnPropertyChanged(nameof(Title));
        }
    }

}
