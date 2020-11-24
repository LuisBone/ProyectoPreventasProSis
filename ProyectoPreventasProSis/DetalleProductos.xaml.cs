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
    public partial class DetalleProductos : ContentPage
    {
        public int Id;
        public DetalleProductos(int id, string nombre, string codigo, int stock, int activo, double precio, string titulo)
        {
            InitializeComponent();

            this.Id = id;
            lblTitulo.Text = titulo;
            lblId.Text = id.ToString();
            txtNombre.Text = nombre;
            txtCodigo.Text = codigo;
            txtStock.Text = stock.ToString();
            txtPrecio.Text = precio.ToString();
            if (activo == 0)
            {
                swActivar.IsToggled = false;
            }
            else
            {
                swActivar.IsToggled = true;
            }
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            string mensaje;

            Ws.Item objeto = new Ws.Item();
            objeto.id = Id;
            objeto.nombre = txtNombre.Text;
            objeto.codigo = txtCodigo.Text;
            objeto.stock = Convert.ToInt32(txtStock.Text);
            objeto.precio = Convert.ToDouble(txtPrecio.Text);

            if(Id == 0)
            {
                objeto.activo = 1;
                mensaje = "Producto creado correctamente.";
            }
            else
            {
                if (swActivar.IsToggled)
                {
                    objeto.activo = 1;
                    mensaje = "Producto actualizado correctamente.";
                }
                else
                {
                    objeto.activo = 0;
                    mensaje = "Producto eliminado correctamente.";
                }
            }
            
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);

                Constantes constante = new Constantes();
                string url;

                if (Id == 0)
                {
                    url = constante.servidor + "crear-producto";
                }
                else
                {
                    url = constante.servidor + "actualizar-producto/" + Id;
                }

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    await DisplayAlert("Ok", mensaje, "ok");
                    await Navigation.PushAsync(new Productos());
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