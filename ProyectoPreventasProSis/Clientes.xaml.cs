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
    public partial class Clientes : ContentPage
    {
        public Clientes()
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
                string url = constante.servidor + "listado-clientes";
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
                    Ws.RootCliente resultado = JsonConvert.DeserializeObject<Ws.RootCliente>(content);

                    List<Ws.ItemCliente> result = resultado.info.items;

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
            var detalle = e.Item as Ws.ItemCliente;
            string titulo = "Editar";
            //await DisplayAlert("prueba", "prueba: "+ detalle.nombre, "ok");
            await Navigation.PushAsync(new DetalleClientes(detalle.id, detalle.identificacion, detalle.nombre, detalle.telefono, detalle.correo, detalle.direccion, detalle.pais, detalle.ciudad,detalle.activo, titulo));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Ws.ItemCliente detalle = new Ws.ItemCliente();
            string titulo = "Crear";
            await Navigation.PushAsync(new DetalleClientes(detalle.id, detalle.identificacion, detalle.nombre, detalle.telefono, detalle.correo, detalle.direccion, detalle.pais, detalle.ciudad, detalle.activo, titulo));
        }




    }
}