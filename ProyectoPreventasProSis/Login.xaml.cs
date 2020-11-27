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
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void btnAbrirVentanaRegistro_Clicked(object sender, EventArgs e)
        {
            string mensaje;

            Ws.Login objeto = new Ws.Login();
            objeto.usuario = txtUser.Text;
            objeto.clave = txtPass.Text;

            try
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objeto);

                Constantes constante = new Constantes();
                string url = constante.servidor + "login-usuario";

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");

                var client = new HttpClient();
                HttpResponseMessage response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    
                    if (content.Contains("ok"))
                    {
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Usuario o clave incorrecta", "ok");
                    }
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