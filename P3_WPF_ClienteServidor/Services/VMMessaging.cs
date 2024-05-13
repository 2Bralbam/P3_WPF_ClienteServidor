using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Services
{
    public class VMMessaging
    {
        public static event EventHandler<string>? CambiarDeVista;
        public static void CambiarVista(string vista)
        {
            CambiarDeVista?.Invoke(null, vista);
        }
    }
}
