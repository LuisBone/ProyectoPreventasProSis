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
    public partial class DetalleCarritoCompras : ContentPage
    {
        public List<Ws.DatosLista> Carrito = new List<Ws.DatosLista>();
        public List<Ws.Item> ListaProducto = new List<Ws.Item>();
        List<dynamic> lista1 = new List<dynamic>();
        public DetalleCarritoCompras(Ws.ItemCliente cliente, Ws.ItemUsuario usuario)
        {
            InitializeComponent();
            txtCliente.Text = cliente.nombre;
            listarProductos();
        }

        public async void listarProductos()
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

                    Ws.Root resultado = JsonConvert.DeserializeObject<Ws.Root>(content);

                    List<Ws.Item> Lista = resultado.info.items;
                    pkProducto.ItemsSource = Lista;

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

        private void pkProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            var selectedItem = picker.SelectedItem;
            Ws.Item ItemSeleccionado = (Ws.Item)selectedItem;
            Ws.DatosLista Item1 = new Ws.DatosLista();
            Item1.producto = ItemSeleccionado;
            Item1.cantidad = 1;
            Item1.precio = ItemSeleccionado.precio;
            Carrito.Add(Item1);

            myListView.ItemsSource = Carrito;

            Button boton = new Button { Text = "X", HorizontalOptions = LayoutOptions.CenterAndExpand, BackgroundColor = Color.Red, FontSize = 35};

            boton.Clicked += DetalleCarritoCompras_Clicked;

            //stacklayout
            StackLayout stackLayout1 = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label { Text = ItemSeleccionado.nombre, VerticalTextAlignment = TextAlignment.Center, Padding = 20, FontSize = 22 },
                    new Entry { Text = "1", Keyboard = Keyboard.Numeric },
                    boton                    
                }
        
            };

            stkProducto.Children.Add(stackLayout1);

        }

        private async void DetalleCarritoCompras_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("ok", "prueba", "ok");
        }

        private void myListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}