using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Models
{
    public class DepartamentoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public int? IdSuperior { get; set; }
    }
}
