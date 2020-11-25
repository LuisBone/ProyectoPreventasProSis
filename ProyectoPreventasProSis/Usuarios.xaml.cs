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
    public partial class Usuarios : ContentPage
    {
        public Usuarios()
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

                    List<Ws.ItemUsuario> result = resultado.info.items;

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
            Ws.ItemUsuario detalle = new Ws.ItemUsuario();
            string titulo = "Crear";
            await Navigation.PushAsync(new DetalleUsuarios(detalle.id, detalle.usuario, detalle.clave, detalle.nombre, detalle.telefono, detalle.correo, detalle.activo, titulo));

        }

        private async void myListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var detalle = e.Item as Ws.ItemUsuario;
            string titulo = "Editar";
            await Navigation.PushAsync(new DetalleUsuarios(detalle.id, detalle.usuario, detalle.clave, detalle.nombre, detalle.telefono, detalle.correo, detalle.activo, titulo));

        }
    }
}