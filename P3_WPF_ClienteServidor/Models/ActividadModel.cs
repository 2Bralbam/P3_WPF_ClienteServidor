using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Models
{
    public class ActividadModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string? Imagen { get; set; }
        public DateOnly? FechaRealizacion { get; set; }
        public string NombreDepartamento { get; set; } = string.Empty;
        public int? Estado { get; set; }
        public string EstadoString { get; set; } = string.Empty;
    }
}
