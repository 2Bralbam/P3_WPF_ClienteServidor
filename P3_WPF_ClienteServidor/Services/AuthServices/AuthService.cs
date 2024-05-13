using Newtonsoft.Json;
using P3_WPF_ClienteServidor.Models;
using System.Net.Http;
using System.Text;
using System.Windows;


namespace P3_WPF_ClienteServidor.Services.AuthServices
{
    public class AuthService
    {
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://registroactividades.websitos256.com/api")
        };
        public AuthService()
        {
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<string?> Login(LoginModel loginModel)
        {
            string JsonModel = JsonConvert.SerializeObject(loginModel);
            StringContent content = new StringContent(JsonModel, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Login", content);
            try
            {
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    
                    return result;
                }
                else
                {
                    MessageBox.Show($"Error al hacer login:{result}","Error");
                    return null;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($"Error al hacer login: {e.Message}","Error");
                return null;
            }
            
        }
    }
}
