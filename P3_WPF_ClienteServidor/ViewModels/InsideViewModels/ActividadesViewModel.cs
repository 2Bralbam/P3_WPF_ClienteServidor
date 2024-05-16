using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class ActividadesViewModel : INotifyPropertyChanged
    {
        public string MirandoDetalles { get; set; } = "False";

        public string MirarAgregar { get; set; } = "False";
        public ICommand VerAgregarCommand { get; set; }
        public ObservableCollection<ActividadModel> ActividadesList { get; set; } = new ObservableCollection<ActividadModel>();
        public ActividadModel ActividadSelected { get; set; } = new ActividadModel();
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ActividadesViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregar);
            VMMessaging.ShowActividadEvent += ShowActividad;
            VMMessaging.HideActividadEvent += HideActividad;
            VMMessaging.HideAgregarActViewEvent += HideAgregarActividad;
            VMMessaging.EliminarActividad += EliminarDeLaLista;
            VMMessaging.AgregarActividadEvent += AgregarActividad;
            DescargarDatos();

        }

        private void AgregarActividad(object? sender, ActividadModel e)
        {
            ActividadesList.Add(e);
            OnPropertyChanged(nameof(ActividadesList));
        }

        private void EliminarDeLaLista(object? sender, int e)
        {
            ActividadesList.Remove(ActividadesList.Where(x => x.Id == e).FirstOrDefault());
            OnPropertyChanged(nameof(ActividadesList));
        }
        private void DescargarDatos()
        {
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
        private void HideAgregarActividad(object? sender, EventArgs e)
        {
            MirarAgregar = "False";
            OnPropertyChanged(nameof(MirarAgregar));

        }

        private void VerAgregar()
        {
            MirarAgregar="True";
            MirandoDetalles = "False";
            OnPropertyChanged(nameof(MirarAgregar));
            OnPropertyChanged(nameof(MirandoDetalles));
        }

        private void HideActividad(object? sender, EventArgs e)
        {
            MirandoDetalles = "False";
            MirarAgregar = "False";
            OnPropertyChanged(nameof(MirandoDetalles));
        }

        private void ShowActividad(object? sender, ActividadModel e)
        {
            MirandoDetalles = "True";
            MirarAgregar = "False";
            OnPropertyChanged(nameof(MirandoDetalles));
        }
    }
}
