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
    public class VerDepDetallesVM:INotifyPropertyChanged
    {
        public DirectoresModel SelectedUser { get; set; } = new DirectoresModel();
        public string Eliminando { get; set; } = "False";
        public string Editando { get; set; } = "False";
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public VerDepDetallesVM()
        {
            VMMessaging.StartingEditing += StartEditing;
            VMMessaging.ShowEditView += Editing;
            VMMessaging.ShowElimination += ShowElimination;
            VMMessaging.ExitElimination += ExitElimination;
            VMMessaging.ExitEditView += ExitEdit;
        }

        private void Editing(object? sender, DirectoresModel e)
        {
            VMMessaging.EditandoDepartamento(e);
            Editando = "True";
            Eliminando = "False";
            OnPropertyChanged(nameof(Editando));
            OnPropertyChanged(nameof(Eliminando));
        }

        private void ExitEdit(object? sender, EventArgs e)
        {
            Eliminando = "False";
            Editando = "False";
            OnPropertyChanged(nameof(Eliminando));
            OnPropertyChanged(nameof(Editando));
        }

        private void ExitElimination(object? sender, EventArgs e)
        {
            Eliminando = "False";
            Editando = "False";
            OnPropertyChanged(nameof(Eliminando));
            OnPropertyChanged(nameof(Editando));
        }

        private void ShowElimination(object? sender, EliminarModel e)
        {
             Eliminando = "True";
            Editando = "False";
            OnPropertyChanged(nameof(Eliminando));
        }

        private void StartEditing(object? sender, DirectoresModel e)
        {
            SelectedUser = e;
            Eliminando = "False";
            Editando = "False";
            OnPropertyChanged(nameof(SelectedUser));
            OnPropertyChanged(nameof(Eliminando));
            OnPropertyChanged(nameof(Editando));
        }
    }
}
