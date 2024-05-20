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

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class AdminDirectoresVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public ObservableCollection<DirectoresModel> AdminDirectoresList { get; set; }
        public string Editing { get; set; } = "False";
        private DataService dataService = new DataService();
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AdminDirectoresVM()
        {
            AdminDirectoresList = new ObservableCollection<DirectoresModel>();
            VMMessaging.StartingEditing += StartEditing;
            VMMessaging.StopEditing += StopEditing;
            VMMessaging.DescargarDepartamentosEvent += async (object? sender, EventArgs e) => { await DescargarDatosDepartamentos(sender, e); };
            VMMessaging.EliminarDepartamentoEvent += EliminarDepartamento;
            DescargarDeps();
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
            IEnumerable<DepartamentoDTO> directores = await dataService.GetDepartamentos();
            var data = directores.Select(x => new DirectoresModel
            {
                Username = x.Nombre,
                Id = x.Id.ToString(),
                Rol = "Director"
            }).ToList();
            AdminDirectoresList.Clear();
            AdminDirectoresList = new ObservableCollection<DirectoresModel>(data);
            OnPropertyChanged(nameof(AdminDirectoresList));
        }

        public async Task DescargarDeps()
        {
            try
            {
                IEnumerable<DepartamentoDTO> directores = await dataService.GetDepartamentos();
                if (directores != null)
                {
                    var data = directores.Select(x => new DirectoresModel
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
