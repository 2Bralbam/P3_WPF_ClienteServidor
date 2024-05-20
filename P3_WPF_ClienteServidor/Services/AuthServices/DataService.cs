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
        public async Task<List<ActividadModel>?> GetData()
        {
            
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                var response = await client.GetAsync("Actividad");
                
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var data = JsonConvert.DeserializeObject<List<ActividadModel>>(result);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public async Task EliminarActividadById(int Id)
        {

            try
            {
                client.DefaultRequestHeaders.Clear();
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
        public async Task PublicarActividad(int Id)
        {
            try
            {
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                //var response = await client.PutAsync($"Actividad/Publicar/{Id}");

                //var result = await response.Content.ReadAsStringAsync();
                //if (response.IsSuccessStatusCode)
                //{
                //    MessageBox.Show("Actividad publicada");
                //}
                //else
                //{
                //    MessageBox.Show("Error al publicar la actividad");
                //}
            }
            catch (Exception e)
            {
            }
        }
        public async Task GuardarActividad(CrearActividadModel CAM)
        {
            try
            {
                client.DefaultRequestHeaders.Clear();
                
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
        public async Task EditarActividad(ActividadModel CAM)
        {
            try
            {
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(CAM);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("Actividad/Editar", content);
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
        public async Task<string> GetImagen(int IdActividad)
        {
            try
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + VMMessaging.TokenJWT);
                var response = await client.GetAsync($"Actividad/{IdActividad}");
                var result = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    string Imagen = JsonConvert.DeserializeObject<ActividadModel>(result).Imagen;
                    return Imagen;
                }
                else
                {
                    MessageBox.Show("Error al obtener la imagen");
                    return null;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al guardar la imagen", e.Message);
                return null;
            }
        }
    }
}
