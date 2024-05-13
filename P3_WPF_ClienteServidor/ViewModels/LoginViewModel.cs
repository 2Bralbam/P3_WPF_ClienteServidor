using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string? Username { get; set; }
        public string? Password { get; set; }
        public RelayCommand LoginCommand { get; set; }
        private readonly IApiCommunication? apiCommunication;
        public LoginViewModel()
        {
            IApiCommunication apiCommunication = new ApiCommunication();
            LoginCommand = new RelayCommand(Login);
        }
        
        private void Login()
        {

            VMMessaging.CambiarVista("DirectorGeneralView");
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
