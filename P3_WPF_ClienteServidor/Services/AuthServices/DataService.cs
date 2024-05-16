using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P3_WPF_ClienteServidor.Services.AuthServices
{
    public class DataService
    {
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://registroactividades.websitos256.com/api/")
        };
        public DataService()
        {
            

        }
        public async Task GetData()
        {
            
            try
            {
                var response = await client.GetAsync("Actividad");
                client.DefaultRequestHeaders.Add("Authorization","Bearer "+VMMessaging.TokenJWT);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                }
            }
            catch (Exception e)
            {
            }

        }
    }
}
