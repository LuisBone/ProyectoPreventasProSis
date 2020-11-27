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
    public partial class DetallePedido : ContentPage
    {
        public List<Ws.ItemCliente> ListaClientes;
        public List<Ws.ItemUsuario> ListaUsuarios;
        public Ws.ItemCliente ClienteSeleccionado;
        public Ws.ItemUsuario UsuarioSeleccionado;
        public int Id = 0;

        public DetallePedido()
        {
            InitializeComponent();

            listarClientes();
            listarUsuarios();
        }

        public async void listarClientes()
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

                    Ws.RootCliente resultado = JsonConvert.DeserializeObject<Ws.RootCliente>(content);

                    ListaClientes = resultado.info.items;
                    pkClientes.ItemsSource = ListaClientes;

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

        public async void listarUsuarios()
        {
            string mensaje;
            try
            {
                Constantes constante = new Constantes();
                string url = constante.servidor + "listado-usuarios";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    Ws.RootUsuario resultado = JsonConvert.DeserializeObject<Ws.RootUsuario>(content);

                    ListaUsuarios = resultado.info.items;
                    pkUsuarios.ItemsSource = ListaUsuarios;

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

        private void pkClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            ClienteSeleccionado = (Ws.ItemCliente)selectedItem;

            //await DisplayAlert("ok", cli.id.ToString(), "ok");

        }

        private void pkUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            UsuarioSeleccionado = (Ws.ItemUsuario)selectedItem;

            //await DisplayAlert("ok", cli.id.ToString(), "ok");
        }

        private async void btnSiguiente_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetalleCarritoCompras(ClienteSeleccionado, UsuarioSeleccionado));
        }
    }
}