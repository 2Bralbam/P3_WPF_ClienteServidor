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
    public class EditarViewModel:INotifyPropertyChanged
    {
        public ActividadModel Actividad { get; set; } = new ActividadModel();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public EditarViewModel()
        {
            VMMessaging.ShowEditarActividadEvent += VMMessaging_ShowEditarActividadEvent;
        }

        private void VMMessaging_ShowEditarActividadEvent(object? sender, ActividadModel e)
        {
            Actividad = e;
        }
    }
}
