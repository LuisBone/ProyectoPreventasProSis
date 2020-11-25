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
    public partial class DetalleUsuarios : ContentPage
    {
        public int Id;
        public string Clave;
        public DetalleUsuarios(int id, string usuario, string clave, string nombre, string telefono, string correo, int activo, string titulo)
        {
            InitializeComponent();

            this.Id = id;
            lblTitulo.Text = titulo;
            Clave = clave;

            txtUsuario.Text = usuario;
            txtNombre.Text = nombre;
            txtTelefono.Text = telefono;
            txtCorreo.Text = correo;
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

            Ws.ItemUsuario objeto = new Ws.ItemUsuario();
            objeto.id = Id;
            objeto.nombre = txtNombre.Text;
            objeto.usuario = txtUsuario.Text;
            objeto.telefono = txtTelefono.Text;
            objeto.correo = txtCorreo.Text;

            if (Id == 0)
            {
                objeto.activo = 1;
                objeto.clave = txtClave.Text;
                mensaje = "Usuario creado correctamente.";
            }
            else
            {
                if (swActivar.IsToggled)
                {
                    objeto.activo = 1;
                    mensaje = "Usuario actualizado correctamente.";
                }
                else
                {
                    objeto.activo = 0;
                    mensaje = "Usuario eliminado correctamente.";
                }
                if(txtClave.Text != null)
                {

                    objeto.clave = txtClave.Text;
                }
                else
                {
                    objeto.clave = this.Clave;
                }
            }
            
            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);

                Constantes constante = new Constantes();
                string url;

                if (Id == 0)
                {
                    url = constante.servidor + "crear-usuario";
                }
                else
                {
                    url = constante.servidor + "actualizar-usuario/" + Id;
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
                    await Navigation.PushAsync(new Usuarios());
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