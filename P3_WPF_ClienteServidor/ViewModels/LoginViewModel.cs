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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace P3_WPF_ClienteServidor.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? Username { get; set; }
        public string? Password { get; set; }
        private string? _errorMessages { get; set; }
        public string? ErrorMessages
        {
            get
            {
                return _errorMessages;
            }
            set
            {
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
            ErrorMessages = string.Empty;
            if (string.IsNullOrWhiteSpace(Username) && string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessages = "Introduzca su correo y contraseña";
                return;
            }
            if (string.IsNullOrWhiteSpace(Username))
            {
                ErrorMessages = "Introduzca su correo\n";
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessages = "Introduzca su contraseña";
            }
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessages = string.Empty;
                LoginModel loginModel = new()
                {
                    email = Username,
                    password = Password
                };
                string? response = await authService.Login(loginModel);
                if (response == null)
                {
                    return;
                }
                response = response?.Replace("\"", "");
                ClaimsPrincipal? claimsPrincipal = ValidateToken(response);
                if (claimsPrincipal != null && !string.IsNullOrEmpty(response))
                {
                    VMMessaging.TokenJWT = response;
                    VMMessaging.UniqueName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                    VMMessaging.IdUsuario = int.Parse(claimsPrincipal.FindFirst("Id")?.Value);
                    string? ROL = claimsPrincipal.FindFirst("idSuperior")?.Value;
                    if (string.IsNullOrEmpty(ROL))
                    {
                        string ADMIN = "TRUE";
                        VMMessaging.IdSuperior = 0;
                    }
                    else
                    {
                        VMMessaging.IdSuperior = int.Parse(claimsPrincipal.FindFirst("idSuperior")?.Value);
                    }
                    
                    
                    VMMessaging.CambiarVista("DirectorGeneralView");
                    VMMessaging.Login();

                }
                else
                {
                    ErrorMessages = "No se inició sesión";
                    return;
                }
                
            }

        }
        public void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
        public static ClaimsPrincipal ValidateToken(string jwtToken)
        {
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidAudience = "ActividadesApp",
                    ValidIssuer = "ApiActividades",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PROGRAMACIONCLIENTESERVIDOR_2024OPORDIOS"))
                };

                SecurityToken validatedToken;
                return new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);
            }
            catch
            {
                return null;
            }
            
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
