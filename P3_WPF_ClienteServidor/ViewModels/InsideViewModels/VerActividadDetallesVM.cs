using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using P3_WPF_ClienteServidor.Services.AuthServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class VerActividadDetallesVM : INotifyPropertyChanged
    {
        public ActividadModel Actividad { get; set; } = new();
        public string Eliminando { get; set; } = "False";
        public BitmapImage DisplayedImage { get; set; }
        public string Editando { get; set; } = "False";
        public ICommand PublicarCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private DataService dataservice = new();
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public VerActividadDetallesVM()
        {
            PublicarCommand = new RelayCommand(async () => { await PublicarActividad(); });
            VMMessaging.ShowActividadEvent += ShowActividad;
            VMMessaging.ShowEditarActividadEvent += ShowEditarActividad;
            VMMessaging.ShowEliminarActividadEvent += ShowEliminarActividad;
            VMMessaging.HideEliminarActividadEvent += HideEliminarActividad;
            VMMessaging.HideEditarActividadEvent += HideEditarActividad;
        }
        private async Task PublicarActividad()
        {
            if (Actividad.Estado == 1)
            {
                MessageBox.Show("La actividad ya está publicada");
            }
            else
            {
                VMMessaging.Login();
                await dataservice.PublicarActividad(Actividad.Id);
                Actividad.EstadoString = "Publicada";
                Actividad.Estado = 1;
                OnPropertyChanged(nameof(Actividad));

            }
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

        private async void ShowActividad(object? sender, ActividadModel e)
        {
            string ActImg = await dataservice.GetImagen(e.Id);
            e.Imagen = ActImg;
            Actividad =e;
            if (e.Imagen != null && e.Imagen != "No tiene imagen")
            {
                ShowImagen();
            }
            else
            {
                DisplayedImage = new BitmapImage();
            }
            OnPropertyChanged(nameof(Actividad));
            OnPropertyChanged(nameof(DisplayedImage));
        }
        private void ShowImagen()
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(Convert.FromBase64String(Actividad.Imagen));
            bitmap.EndInit();
            DisplayedImage = bitmap;
        }
    }
}
