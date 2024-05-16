using P3_WPF_ClienteServidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Services
{
    public static class VMMessaging
    {
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
                throw new ArgumentNullException(nameof(D));
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
    }
}
