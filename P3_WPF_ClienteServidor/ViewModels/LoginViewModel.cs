using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Services;
using P3_WPF_ClienteServidor.Services.AuthServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using P3_WPF_ClienteServidor.Models;

namespace P3_WPF_ClienteServidor.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string? Username { get; set; }
        public string? Password { get; set; }
        private string? _errorMessages { get; set; }
        public string? ErrorMessages {
            get
            {
                return _errorMessages;
            }
            set {
                _errorMessages = value;
                OnPropertyChanged(nameof(ErrorMessages));
            }
        }
        public RelayCommand LoginCommand { get; set; }
        private AuthService authService = new AuthService();
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }
        
        
        private async void Login()
        {
            //ErrorMessages = string.Empty;
            //if(string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
            //{
            //    ErrorMessages = "Introduzca su correo y contraseña";
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(Username))
            //{
            //    ErrorMessages = "Introduzca su correo\n";
            //}
            //if(string.IsNullOrWhiteSpace(Password))
            //{
            //    ErrorMessages = "Introduzca su contraseña";
            //}
            //if(!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            //{
            //    ErrorMessages=string.Empty;
            //    LoginModel loginModel = new()
            //    {
            //        name = Username,
            //        password = Password
            //    };
            //    string? response = await authService.Login(loginModel);
            //    if(response == null)
            //    {
            //        ErrorMessages = "No se inició sesión";
            //        return;
            //    }
            //    else
            //    {

            //    }
               VMMessaging.CambiarVista(Username);
            //}
            
        }
        public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
