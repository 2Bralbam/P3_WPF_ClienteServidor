using Newtonsoft.Json;
using P3_WPF_ClienteServidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                var response = await client.GetAsync("Actividad");
                
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
        public async Task EliminarActividadById(int Id)
        {

            try
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                var response = await client.DeleteAsync($"Actividad/Eliminar/{Id}");

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
        public async Task GuardarActividad(CrearActividadModel CAM)
        {
            try
            {
                
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(CAM);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Actividad",content);
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                }
                else
                {
                    string error = await response.Content.ReadAsStringAsync();
                    MessageBox.Show("Error al guardar la actividad", error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al guardar la actividad", e.Message);
            }
        }
    }
}
