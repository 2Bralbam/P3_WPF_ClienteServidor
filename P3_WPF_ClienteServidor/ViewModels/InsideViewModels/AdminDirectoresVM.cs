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
    public class AdminDirectoresVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public List<DirectoresModel> AdminDirectoresList { get; set; }
        public string Editing { get; set; } = "False";
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AdminDirectoresVM()
        {
            AdminDirectoresList = new List<DirectoresModel>();
            AdminDirectoresList.Add(new DirectoresModel { Username = "Director1", Id = "1", Rol = "Director" });
            AdminDirectoresList.Add(new DirectoresModel { Username = "Director2", Id = "2", Rol = "Director" });
            AdminDirectoresList.Add(new DirectoresModel { Username = "Director3", Id = "3", Rol = "Director" });
            VMMessaging.StartingEditing += StartEditing;
            VMMessaging.StopEditing += StopEditing;
        }

        private void StopEditing(object? sender, EventArgs e)
        {
            Editing = "False";
            OnPropertyChanged(nameof(Editing));
        }

        private void StartEditing(object? sender, DirectoresModel e)
        {
            Editing = "True";
            OnPropertyChanged(nameof(Editing));
        }
    }
}
