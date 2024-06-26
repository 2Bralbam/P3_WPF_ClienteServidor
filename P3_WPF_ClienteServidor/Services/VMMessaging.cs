﻿using P3_WPF_ClienteServidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Services
{
    public static class VMMessaging
    {
        public static List<DirectoresModel> ListaDeDepartamentos { get; set; }
        public static DirectoresModel? SelectedUser { get; set; }
        public static ActividadModel? SelectedActividad { get; set; }
        public static string UniqueName { get; set; } = null!;
        public static int IdSuperior { get; set; }
        public static int IdUsuario { get; set; }

        public static string? TokenJWT { get; set; }
        public static event EventHandler<string>? CambiarDeVista;
        public static void CambiarVista(string vista)
        {
            CambiarDeVista?.Invoke(null, vista);
        }
        public static event EventHandler<DirectoresModel>? StartingEditing;
        public static void EditingUser(DirectoresModel D)
        {
            if (D == null)
            {

            }
            SelectedUser = D;
            StartingEditing?.Invoke(null, D);
        }
        public static event EventHandler? StopEditing;
        public static void ExitEditing()
        {
            StopEditing?.Invoke(null, null);

        }
        public static event EventHandler<DirectoresModel>? ShowEditView;
        public static void ShowEdit(DirectoresModel DM)
        {
            ShowEditView?.Invoke(null, DM);

        }
        public static event EventHandler? ExitEditView;
        public static void ExitEditV()
        {
            ExitEditView?.Invoke(null, null);

        }
        public static event EventHandler<EliminarModel>? ShowElimination;
        public static void ShowElim(EliminarModel eM)
        {
            ShowElimination?.Invoke(null, eM);

        }
        public static event EventHandler? ExitElimination;
        public static void ExitElim()
        {
            ExitElimination?.Invoke(null, null);

        }
        public static event EventHandler<ActividadModel>? ShowActividadEvent;
        public static void ShowActividad(ActividadModel aM)
        {
            SelectedActividad = aM;
            ShowActividadEvent?.Invoke(null, aM);

        }
        public static EventHandler? HideActividadEvent;
        public static void HideActividadDetallesView()
        {
            HideActividadEvent?.Invoke(null, null);

        }
        public static event EventHandler<ActividadModel>? ShowEditarActividadEvent;
        public static void ShowEditarActividad(ActividadModel aM)
        {
            SelectedActividad = aM;
            ShowEditarActividadEvent?.Invoke(null, aM);

        }
        public static EventHandler? HideEditarActividadEvent;
        public static void HideEditarACtividad()
        {
            HideEditarActividadEvent?.Invoke(null, null);

        }
        public static event EventHandler<ActividadModel>? ShowEliminarActividadEvent;
        public static void ShowEliminarActividad(ActividadModel aM)
        {
            SelectedActividad = aM;
            ShowEliminarActividadEvent?.Invoke(null, aM);

        }
        public static EventHandler? HideEliminarActividadEvent;
        public static void HideEliminarActividad()
        {
            HideEliminarActividadEvent?.Invoke(null, null);

        }
        public static EventHandler? HideAgregarActViewEvent;
        public static void HideAgregarActView()
        {
            HideAgregarActViewEvent?.Invoke(null, null);

        }
        public static EventHandler<int>? EliminarActividad;
        public static void EliminarActividadById(int id)
        {
            EliminarActividad?.Invoke(null, id);

        }
        public static EventHandler<ActividadModel>? AgregarActividadEvent;
        public static void AgregarActividad(ActividadModel aM)
        {
            AgregarActividadEvent?.Invoke(null, aM);

        }
        public static EventHandler? LoginEvent;
        public static void Login()
        {
            LoginEvent?.Invoke(null, null);

        }
        public static EventHandler<ActividadModel>? EditActividadEvent;
        public static void EditActividad(ActividadModel aM)
        {
            EditActividadEvent?.Invoke(null, aM);

        }
        public static EventHandler? DescargarDepartamentosEvent;
        public static void DescargarDepartamentos()
        {
            DescargarDepartamentosEvent?.Invoke(null, null);

        }
        public static EventHandler<DirectoresModel>? EditandoDepartamentoEvent;
        public static void EditandoDepartamento(DirectoresModel d)
        {
            EditandoDepartamentoEvent?.Invoke(null, d);

        }
        public static EventHandler<DirectoresModel>? AgregarDepartamentoEvent;
        public static void AgregarDepartamento(DirectoresModel d)
        {
            AgregarDepartamentoEvent?.Invoke(null, d);

        }
        public static EventHandler? AgregandoDepartamentoEvent;
        public static void AgregandoDepartamento()
        {
            AgregandoDepartamentoEvent?.Invoke(null, null);

        }
        public static EventHandler<DirectoresModel>? EliminarDepartamentoEvent;
        public static void EliminarDepartamento(DirectoresModel d)
        {
            EliminarDepartamentoEvent?.Invoke(null, d);

        }
        public static EventHandler<int>? PublicarActividadEvent;
        public static void PublicarActividad(int id)
        {
            PublicarActividadEvent?.Invoke(null, id);

        }
        public static EventHandler? HideAgregarDepartamentoEvent;
        public static void HideAgregarDepartamento()
        {
            HideAgregarDepartamentoEvent?.Invoke(null, null);

        }
        public static EventHandler? CheckUserAdminEvent;
        public static void CheckUserAdmin()
        {
            CheckUserAdminEvent?.Invoke(null, null);

        }
    } 
}
