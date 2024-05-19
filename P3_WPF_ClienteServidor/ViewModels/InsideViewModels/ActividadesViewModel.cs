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
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace P3_WPF_ClienteServidor.ViewModels.InsideViewModels
{
    public class ActividadesViewModel : INotifyPropertyChanged
    {
        public string MirandoDetalles { get; set; } = "False";

        public string MirarAgregar { get; set; } = "False";
        public ICommand VerAgregarCommand { get; set; }
        public ObservableCollection<string> DepartamentosFiltro { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> EstadosFiltro { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> FechasRealizacion { get; set; } = new ObservableCollection<string>();
        public string DepartamentoSeleccionado { get; set; } = "";
        public string EstadoSeleccionado { get; set; } = "";
        public string FechaSeleccionada { get; set; } = "";
        public int SelectedIndexDepSelect { get; set; } = 0;
        public int SelectedIndexEstSelect { get; set; } = 0;
        public int SelectedIndexFechaSelect { get; set; } = 0;
        public ObservableCollection<ActividadModel> ActividadesList { get; set; } = new ObservableCollection<ActividadModel>();
        private DataService dataService = new DataService();
        private List<ActividadModel> originalActList { get; set; } = new List<ActividadModel>();
        public ActividadModel ActividadSelected { get; set; } = new ActividadModel();
        public ICommand FiltrarCommand { get; set; }
        public ICommand LimpiarFiltroCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ActividadesViewModel()
        {
            VerAgregarCommand = new RelayCommand(VerAgregar);
            FiltrarCommand = new RelayCommand(Filtrar);
            LimpiarFiltroCommand = new RelayCommand(LimpiarFiltro);
            VMMessaging.ShowActividadEvent += ShowActividad;
            VMMessaging.HideActividadEvent += HideActividad;
            VMMessaging.HideAgregarActViewEvent += HideAgregarActividad;
            VMMessaging.EliminarActividad += EliminarDeLaLista;
            VMMessaging.AgregarActividadEvent += AgregarActividad;
            VMMessaging.LoginEvent += async (object? sender, EventArgs e) => { await DescargarDatos(sender,e); };

        }

        private void AgregarActividad(object? sender, ActividadModel e)
        {
            Application.Current.Dispatcher.Invoke(async() =>
            {

                await UpdateData();
            });
        }

        private void EliminarDeLaLista(object? sender, int e)
        {
            try
            {
                ActividadesList.Remove(ActividadesList.Where(x => x.Id == e).FirstOrDefault());
                originalActList.Remove(originalActList.Where(x => x.Id == e).FirstOrDefault());
                OnPropertyChanged(nameof(ActividadesList));
            }
            catch
            {

            }
            
        }
        private void Filtrar()
        {
            if (originalActList != null)
            {
                var filteredList = originalActList.AsEnumerable();

                if (!string.IsNullOrEmpty(DepartamentoSeleccionado))
                {
                    filteredList = filteredList.Where(x => x.NombreDepartamento == DepartamentoSeleccionado);
                }

                if (!string.IsNullOrEmpty(EstadoSeleccionado))
                {
                    filteredList = filteredList.Where(x => x.Estado.ToString() == EstadoSeleccionado);
                }

                if (!string.IsNullOrEmpty(FechaSeleccionada))
                {
                    filteredList = filteredList.Where(x => x.FechaRealizacion.ToString() == FechaSeleccionada);
                }

                ActividadesList = new ObservableCollection<ActividadModel>(filteredList.OrderBy(x=>x.Id).Where(x => x.Estado != 2));
                OnPropertyChanged(nameof(ActividadesList));
            }
        }
        private void LimpiarFiltro()
        {
            SelectedIndexDepSelect = 0;
            SelectedIndexEstSelect = 0;
            SelectedIndexFechaSelect = 0;
            if (originalActList != null)
            {
                ActividadesList = new ObservableCollection<ActividadModel>(originalActList.OrderBy(x => x.Id).Where(x => x.Estado != 2).ToList());
                OnPropertyChanged(nameof(ActividadesList));
                OnPropertyChanged(nameof(SelectedIndexDepSelect));
                OnPropertyChanged(nameof(SelectedIndexEstSelect));
                OnPropertyChanged(nameof(SelectedIndexFechaSelect));
            }
            
        }
        private async Task DescargarDatos(object? sender, EventArgs e)
        {
            try
            {
                var Data = await dataService.GetData();
                FechasRealizacion.Clear();
                DepartamentosFiltro.Clear();
                EstadosFiltro.Clear();
                if (Data != null)
                {
                    originalActList = new List<ActividadModel>(Data);
                    Parallel.ForEach(Data, x =>
                    {
                        if(x.Estado == 1)
                        {
                            x.EstadoString = "Publicada";
                        }
                        else if (x.Estado == 2)
                        {
                            x.EstadoString = "Eliminada";
                        }
                        else if (x.Estado == 0)
                        {
                            x.EstadoString = "Borrador";
                        }
                    });
                    ActividadesList = new ObservableCollection<ActividadModel>(Data.OrderBy(x => x.Id).Where(x=>x.Estado!=2));
                    
                    OnPropertyChanged(nameof(ActividadesList));
                }
                else
                {

                    ActividadesList = new ObservableCollection<ActividadModel>();
                    OnPropertyChanged(nameof(ActividadesList));
                }
                var StringsFechas = ActividadesList.Where(x => x.Estado != 2).DistinctBy(x=>x.FechaRealizacion).Select(x => x.FechaRealizacion.ToString()).ToList();
                StringsFechas.Insert(0, "");
                var StringsDepartamentos = ActividadesList.Where(x => x.Estado != 2).DistinctBy(x=>x.NombreDepartamento).Select(x => x.NombreDepartamento).ToList();
                StringsDepartamentos.Insert(0, "");
                var StringsEstados = ActividadesList.Where(x => x.Estado != 2).DistinctBy(x=>x.Estado).Select(x => x.Estado.ToString()).ToList();
                StringsEstados.Insert(0, "");
                foreach (var x in ActividadesList)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DepartamentosFiltro = new ObservableCollection<string>(StringsDepartamentos);
                        EstadosFiltro = new ObservableCollection<string>(StringsEstados);
                        FechasRealizacion = new ObservableCollection<string>(StringsFechas);

                    });
                }
                OnPropertyChanged(nameof(FechasRealizacion));
                OnPropertyChanged(nameof(DepartamentosFiltro));
                OnPropertyChanged(nameof(EstadosFiltro));
            }
            catch
            {

            }
            
            //ActividadesList.Add(
            //    new ActividadModel
            //    {
            //        fechaActualizacion = DateTime.Now,
            //        fechaCreacion = DateTime.Now,
            //        Id = 1,
            //        Titulo = "Actividad 1",
            //        Descripcion = "Descripcion 1",
            //        Estado = 1,
            //        FechaRealizacion = "AYER",
            //        IdDep = 1,
            //        IdUser = 1


            //    }
            //);
            //ActividadesList.Add(
            //                   new ActividadModel
            //                   {
            //                       fechaActualizacion = DateTime.Now,
            //                       fechaCreacion = DateTime.Now,
            //                       Id = 2,
            //                       Titulo = "Actividad 2",
            //                       Descripcion = "Descripcion 2",
            //                       Estado = 1,
            //                       FechaRealizacion = "HOY",
            //                       IdDep = 2,
            //                       IdUser = 2
            //                   }
            //                              );

        }
        private async Task UpdateData()
        {
            try
            {
                var Data = await dataService.GetData();
                FechasRealizacion.Clear();
                DepartamentosFiltro.Clear();
                EstadosFiltro.Clear();
                if (Data != null)
                {
                    originalActList = new List<ActividadModel>(Data);
                    Parallel.ForEach(Data, x =>
                    {
                        if (x.Estado == 1)
                        {
                            x.EstadoString = "Publicada";
                        }
                        else if (x.Estado == 2)
                        {
                            x.EstadoString = "Eliminada";
                        }
                        else if (x.Estado == 0)
                        {
                            x.EstadoString = "Borrador";
                        }
                    });
                    ActividadesList = new ObservableCollection<ActividadModel>(Data.OrderBy(x => x.Id).Where(x => x.Estado != 2));

                    OnPropertyChanged(nameof(ActividadesList));
                }
                else
                {

                    ActividadesList = new ObservableCollection<ActividadModel>();
                    OnPropertyChanged(nameof(ActividadesList));
                }
                var StringsFechas = ActividadesList.Where(x => x.Estado != 2).DistinctBy(x => x.FechaRealizacion).Select(x => x.FechaRealizacion.ToString()).ToList();
                StringsFechas.Insert(0, "");
                var StringsDepartamentos = ActividadesList.Where(x => x.Estado != 2).DistinctBy(x => x.NombreDepartamento).Select(x => x.NombreDepartamento).ToList();
                StringsDepartamentos.Insert(0, "");
                var StringsEstados = ActividadesList.Where(x => x.Estado != 2).DistinctBy(x => x.Estado).Select(x => x.Estado.ToString()).ToList();
                StringsEstados.Insert(0, "");
                foreach (var x in ActividadesList)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        DepartamentosFiltro = new ObservableCollection<string>(StringsDepartamentos);
                        EstadosFiltro = new ObservableCollection<string>(StringsEstados);
                        FechasRealizacion = new ObservableCollection<string>(StringsFechas);

                    });
                }
                OnPropertyChanged(nameof(FechasRealizacion));
                OnPropertyChanged(nameof(DepartamentosFiltro));
                OnPropertyChanged(nameof(EstadosFiltro));
            }
            catch
            {

            }

            //ActividadesList.Add(
            //    new ActividadModel
            //    {
            //        fechaActualizacion = DateTime.Now,
            //        fechaCreacion = DateTime.Now,
            //        Id = 1,
            //        Titulo = "Actividad 1",
            //        Descripcion = "Descripcion 1",
            //        Estado = 1,
            //        FechaRealizacion = "AYER",
            //        IdDep = 1,
            //        IdUser = 1


            //    }
            //);
            //ActividadesList.Add(
            //                   new ActividadModel
            //                   {
            //                       fechaActualizacion = DateTime.Now,
            //                       fechaCreacion = DateTime.Now,
            //                       Id = 2,
            //                       Titulo = "Actividad 2",
            //                       Descripcion = "Descripcion 2",
            //                       Estado = 1,
            //                       FechaRealizacion = "HOY",
            //                       IdDep = 2,
            //                       IdUser = 2
            //                   }
            //                              );

        }
        private void HideAgregarActividad(object? sender, EventArgs e)
        {
            MirarAgregar = "False";
            OnPropertyChanged(nameof(MirarAgregar));

        }

        private void VerAgregar()
        {
            MirarAgregar="True";
            MirandoDetalles = "False";
            OnPropertyChanged(nameof(MirarAgregar));
            OnPropertyChanged(nameof(MirandoDetalles));
        }

        private void HideActividad(object? sender, EventArgs e)
        {
            MirandoDetalles = "False";
            MirarAgregar = "False";
            OnPropertyChanged(nameof(MirandoDetalles));
        }

        private void ShowActividad(object? sender, ActividadModel e)
        {
            MirandoDetalles = "True";
            MirarAgregar = "False";
            OnPropertyChanged(nameof(MirandoDetalles));
        }
    }
}
