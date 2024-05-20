using CommunityToolkit.Mvvm.Input;
using P3_WPF_ClienteServidor.Models;
using P3_WPF_ClienteServidor.Services;
using P3_WPF_ClienteServidor.Services.AuthServices;
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
    public class AdminDirectoresVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<DirectoresModel> AdminDirectoresList { get; set; }
        public bool IsAdmin { get {
                if (VMMessaging.IdSuperior == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public string AgregandoDepartamento { get; set; } = "False";
        public ICommand AgregarDepartamentoCommand { get; set; }
        public string Editing { get; set; } = "False";
        private DataService dataService = new DataService();
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AdminDirectoresVM()
        {
            AgregarDepartamentoCommand = new RelayCommand(() => { AgregandoDepartamento = "True"; OnPropertyChanged(nameof(AgregandoDepartamento)); VMMessaging.AgregandoDepartamento(); });
            AdminDirectoresList = new ObservableCollection<DirectoresModel>();
            VMMessaging.StartingEditing += StartEditing;
            VMMessaging.StopEditing += StopEditing;
            VMMessaging.DescargarDepartamentosEvent += async (object? sender, EventArgs e) => { await DescargarDatosDepartamentos(sender, e); };
            VMMessaging.EliminarDepartamentoEvent += EliminarDepartamento;
            VMMessaging.HideAgregarDepartamentoEvent += HideAgregarDepartamento;
            VMMessaging.AgregarDepartamentoEvent += AgregarDepartamento;
            VMMessaging.CheckUserAdminEvent += CheckUserAdmin;
            DescargarDeps();
        }

        private void CheckUserAdmin(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsAdmin));
        }

        private void AgregarDepartamento(object? sender, DirectoresModel e)
        {
            DescargarDatosDepartamentos(sender, new EventArgs());
            OnPropertyChanged(nameof(IsAdmin));
        }

        private void HideAgregarDepartamento(object? sender, EventArgs e)
        {
            AgregandoDepartamento = "False";
            OnPropertyChanged(nameof(AgregandoDepartamento));
        }

        private void EliminarDepartamento(object? sender, DirectoresModel e)
        {

            try
            {
                int IndexDep = AdminDirectoresList.IndexOf(AdminDirectoresList.FirstOrDefault(x => x.Id == e.Id));
                AdminDirectoresList.RemoveAt(IndexDep);
                OnPropertyChanged(nameof(AdminDirectoresList));
                Editing="False";
                OnPropertyChanged(nameof(Editing));
            }
            catch
            {

            }
            
        }

        private async Task DescargarDatosDepartamentos(object? sender, EventArgs e)
        {
            try
            {
                OnPropertyChanged(nameof(IsAdmin));
                IEnumerable<DepartamentoDTO> directores = await dataService.GetDepartamentos();
                if(directores != null)
                {
                    var data = directores.Where(x=>x.Id != VMMessaging.IdUsuario && x.IdSuperior >= VMMessaging.IdUsuario).Select(x => new DirectoresModel
                    {
                        Username = x.Nombre,
                        Id = x.Id.ToString(),
                        Rol = "Director"
                    }).ToList();
                    AdminDirectoresList.Clear();
                    AdminDirectoresList = new ObservableCollection<DirectoresModel>(data);
                    OnPropertyChanged(nameof(AdminDirectoresList));

                }
                else
                {
                    AdminDirectoresList.Clear();
                    OnPropertyChanged(nameof(AdminDirectoresList));

                }

            }
            catch (Exception ex)
            {

            }
        }

        public async Task DescargarDeps()
        {
            try
            {
                IEnumerable<DepartamentoDTO> directores = await dataService.GetDepartamentos();
                if (directores != null)
                {
                    var data = directores.Where(x => x.Id != VMMessaging.IdUsuario && x.IdSuperior >= VMMessaging.IdUsuario).Select(x => new DirectoresModel
                    {
                        Username = x.Nombre,
                        Id = x.Id.ToString(),
                        Rol = x.Correo
                    }).ToList();
                    VMMessaging.ListaDeDepartamentos = new List<DirectoresModel>(data);
                    AdminDirectoresList.Clear();
                    AdminDirectoresList = new ObservableCollection<DirectoresModel>(data);
                    OnPropertyChanged(nameof(AdminDirectoresList));
                }
            }
            catch (Exception ex)
            {

            }
            
        }
        public async Task EditarDepartamento(DepartamentoDTO departamento)
        {
            //await dataService.EditarDepartamento(departamento);
            await DescargarDeps();
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
