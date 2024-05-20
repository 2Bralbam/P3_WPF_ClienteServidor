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
using System.Windows;
using System.Windows.Input;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class EditarViewModel:INotifyPropertyChanged
    {
        public ActividadModel EditedAct { get; set; } = new ActividadModel();
        public ActividadModel Actividad { get; set; } = new ActividadModel();
        public string FotoPath { get; set; } = string.Empty;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ConfirmarEditarCommand { get; set; }
        public ICommand CancelarEditarCommand { get; set; }
        public ICommand SelectFotoCommand { get; set; }
        private DataService DataService { get; set; } = new DataService();
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public EditarViewModel()
        {
            SelectFotoCommand = new RelayCommand(SeleccionarFoto);
            CancelarEditarCommand = new RelayCommand(() =>
            {
                
                OnPropertyChanged(nameof(Actividad));

                VMMessaging.HideEditarACtividad();
            });
            ConfirmarEditarCommand = new RelayCommand(ConfirmarEditar);
            VMMessaging.ShowEditarActividadEvent += VMMessaging_ShowEditarActividadEvent;
        }

        private void ConfirmarEditar()
        {
            string Errores = string.Empty;
            if (string.IsNullOrEmpty(EditedAct.EstadoString)) {
                Errores += "El estado no puede estar vacío\n";
            }
            if (string.IsNullOrEmpty(EditedAct.Titulo))
            {
                Errores += "El título no puede estar vacío\n";
            }
            if (string.IsNullOrEmpty(EditedAct.Descripcion))
            {
                Errores += "La descripción no puede estar vacía\n";
            }
            if (string.IsNullOrEmpty(EditedAct.Imagen))
            {
                Errores += "La imagen no puede estar vacía\n";
            }
            if (Errores != string.Empty)
            {
                MessageBox.Show(Errores);
                return;
            }
            else
            {
                VMMessaging.EditActividad(EditedAct);
                Task.Run(async () =>
                {
                    await DataService.EditarActividad(Actividad);
                });
                Actividad = EditedAct;
                FotoPath = string.Empty;
                OnPropertyChanged(nameof(FotoPath));
                VMMessaging.HideEditarACtividad();
            }
            VMMessaging.Login();
            VMMessaging.HideActividadDetallesView();
        }

        private void VMMessaging_ShowEditarActividadEvent(object? sender, ActividadModel e)
        {
            ActividadModel eAct = new() 
            { 
                Estado = e.Estado,
                EstadoString = e.EstadoString,
                FechaRealizacion = e.FechaRealizacion,
                Descripcion = e.Descripcion,
                Id = e.Id,
                Imagen = e.Imagen,
                NombreDepartamento = e.NombreDepartamento,
                Titulo = e.Titulo
            };
            EditedAct = eAct;
            Actividad = e;
            OnPropertyChanged(nameof(EditedAct));
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
                EditedAct.Imagen = base64String;
                // Ahora puedes usar 'base64String' como necesites
            }
        }
    }
}
