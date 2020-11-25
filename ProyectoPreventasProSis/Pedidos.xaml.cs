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
    public partial class Pedidos : ContentPage
    {
        public List<Ws.ItemCliente> ListaClientes;
        public List<Ws.ItemUsuario> ListaUsuarios;
        public Pedidos()
        {
            InitializeComponent();
            listarPedidos();
        }

        public async void listarPedidos()
        {
            string mensaje;
            try
            {
                Constantes constante = new Constantes();
                string url = constante.servidor + "listado-pedidos";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Ws.RootPedido resultado = JsonConvert.DeserializeObject<Ws.RootPedido>(content);

                    List<Ws.ItemPedido> result = resultado.info.items;

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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new DetallePedido());
        }

        private async void myListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalle = e.Item as Ws.ItemPedido;
            string titulo = "Editar";
        }
    }
}