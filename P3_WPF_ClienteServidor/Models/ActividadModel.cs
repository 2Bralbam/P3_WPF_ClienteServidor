using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Models
{
    public class ActividadModel
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string FechaRealizacion { get; set; } = null!;
        public DateTime fechaCreacion { get; set; } 
        public DateTime fechaActualizacion { get; set; } 
        public int Id { get; set; } 
        public int IdUser { get; set; } 
        public int IdDep { get; set; } 
        public int Estado { get; set; }
    }
}
