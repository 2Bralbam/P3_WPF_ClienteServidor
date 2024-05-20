using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
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
        public string FotoPath { get; set; } = string.Empty;
        public string FotoBase64 { get; set; } = string.Empty;
        public string Titulo { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public ICommand AgregarActividadCommand { get; set; }
        public ICommand SeleccionarFotoCommand { get; set; }
        private DataService dataService = new DataService();
        public AgregarActividadVM()
        {
            SeleccionarFotoCommand = new RelayCommand(()=> { SeleccionarFoto(); });
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
                FechaRealizacion = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd")),
                Imagen = FotoPath
            };
            VMMessaging.AgregarActividad(actividadModel);
        }
        private void SeleccionarFoto()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.Title = "Selecciona una imagen";

            if (openFileDialog.ShowDialog() == true)
            {
                // Obtén la ruta del archivo seleccionado
                string imagePath = openFileDialog.FileName;
                FotoPath = imagePath;
                OnPropertyChanged(nameof(FotoPath));
                // Lee la imagen como un arreglo de bytes
                byte[] imageBytes = File.ReadAllBytes(imagePath);

                // Convierte los bytes de la imagen a Base64
                string base64String = Convert.ToBase64String(imageBytes);
                FotoBase64 = base64String;
                // Ahora puedes usar 'base64String' como necesites
            }
        }

    }
}
