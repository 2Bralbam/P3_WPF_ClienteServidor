using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Services
{
    public class ApiCommunication:IApiCommunication
    {
        HttpClient HttpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://registroactividades.websitos256.com/api")
        };
    }
}
