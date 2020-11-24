using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoPreventasProSis
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Productos : ContentPage
    {
        public Productos()
        {
            InitializeComponent();

            iniciar();
        }

        public async void iniciar()
        {
            string mensaje;
            try
            {
                Constantes constante = new Constantes();
                string url = constante.servidor + "listado-productos";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //lblError.Text = content;
                    //var resultado = JsonConvert.DeserializeObject<List<Ws.Root>>(content);
                    Ws.Root resultado = JsonConvert.DeserializeObject<Ws.Root>(content);

                    List<Ws.Item> result = resultado.info.items;

                    myListView.ItemsSource = result;
                }
                else
                {
                    mensaje = "Problema: " + response.StatusCode;
                    await DisplayAlert("Error", mensaje, "ok");
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex;
                await DisplayAlert("Error", mensaje, "ok");
            }

        }

        private async void myListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalle = e.Item as Ws.Item;
            //await DisplayAlert("prueba", "prueba: "+ detalle.nombre, "ok");
            await Navigation.PushAsync(new DetalleProductos(detalle.id, detalle.nombre, detalle.codigo, detalle.stock, detalle.activo, detalle.precio));
        }

        private async void btnConsulta_Clicked(object sender, EventArgs e)
        {
            string mensaje;
            try
            {
                Constantes constante = new Constantes();
                string url = constante.servidor + "listado-productos";
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    //lblError.Text = content;
                    //var resultado = JsonConvert.DeserializeObject<List<Ws.Root>>(content);
                    Ws.Root resultado = JsonConvert.DeserializeObject<Ws.Root>(content);

                    List<Ws.Item> result = resultado.info.items;

                    myListView.ItemsSource = result;
                }
                else
                {
                    mensaje = "Problema: " + response.StatusCode;
                    await DisplayAlert("Error", mensaje, "ok");
                }
            }
            catch (Exception ex)
            {
                mensaje = "Error: " + ex;
                await DisplayAlert("Error", mensaje, "ok");
            }
        }
    }
}