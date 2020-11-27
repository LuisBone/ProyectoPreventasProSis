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
    public partial class DetalleClientes : ContentPage
    {
        public int Id;
        public DetalleClientes(int id, string identificacion, string nombre, string telefono, string correo, string direccion, string pais, string ciudad , int activo, string titulo)
        {
            InitializeComponent();

            this.Id = id;
            lblTitulo.Text = titulo;
            lblId.Text = id.ToString();
            txtIdentificacion.Text = identificacion;
            txtNombre.Text = nombre;
            txtTelefono.Text = telefono;
            txtCorreo.Text = correo;
            txtDireccion.Text = direccion;
            txtPais.Text = pais;
            txtCiudad.Text = ciudad;

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

            Ws.ItemCliente objeto = new Ws.ItemCliente();
            objeto.id = Id;
            objeto.identificacion = txtIdentificacion.Text;
            objeto.nombre = txtNombre.Text;
            objeto.telefono = txtTelefono.Text;
            objeto.correo = txtCorreo.Text;
            objeto.direccion = txtDireccion.Text;
            objeto.pais = txtPais.Text;
            objeto.ciudad= txtCiudad.Text;

            if (Id == 0)
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
                    url = constante.servidor + "crear-cliente";
                }
                else
                {
                    url = constante.servidor + "actualizar-cliente/" + Id;
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