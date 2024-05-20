using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Models
{
    public class DirectoresModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private string _id { get; set; }
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        public string _rol { get; set; }
        public string Rol
        {
            get { return _rol; }
            set
            {
                _rol = value;
                OnPropertyChanged(nameof(Rol));
            }
        }

        public string _nombre { get; set; }
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                _nombre = value;
                OnPropertyChanged(nameof(Nombre));
            }
        }

        public string _username { get; set; }
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string _password { get; set; }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public int _idSuperior { get; set; }
        public int IdSuperior
        {
            get { return _idSuperior; }
            set
            {
                _idSuperior = value;
                OnPropertyChanged(nameof(IdSuperior));
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}