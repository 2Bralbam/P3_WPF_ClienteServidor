using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class ActividadesViewModel : INotifyPropertyChanged
    {
        public string MirandoDetalles { get; set; } = "False";
 
        public ObservableCollection<ActividadModel> ActividadesList { get; set; } = new ObservableCollection<ActividadModel>();
        public ActividadModel ActividadSelected { get; set; } = new ActividadModel();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ActividadesViewModel()
        {
            VMMessaging.ShowActividadEvent += ShowActividad;
            VMMessaging.HideActividadEvent += HideActividad;
            ActividadesList.Add(
                new ActividadModel
                {
                    fechaActualizacion = DateTime.Now,
                    fechaCreacion = DateTime.Now,
                    Id = 1,
                    Titulo = "Actividad 1",
                    Descripcion = "Descripcion 1",
                    Estado = 1,
                    FechaRealizacion = "AYER",
                    IdDep = 1,
                    IdUser = 1


                }
            );
            ActividadesList.Add(
                               new ActividadModel
                               {
                                   fechaActualizacion = DateTime.Now,
                                   fechaCreacion = DateTime.Now,
                                   Id = 2,
                                   Titulo = "Actividad 2",
                                   Descripcion = "Descripcion 2",
                                   Estado = 1,
                                   FechaRealizacion = "HOY",
                                   IdDep = 2,
                                   IdUser = 2
                               }
                                          );

        }

        private void HideActividad(object? sender, EventArgs e)
        {
            MirandoDetalles = "False";
            OnPropertyChanged(nameof(MirandoDetalles));
        }

        private void ShowActividad(object? sender, ActividadModel e)
        {
            MirandoDetalles = "True";
            OnPropertyChanged(nameof(MirandoDetalles));
        }
    }
}
