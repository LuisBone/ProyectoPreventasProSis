using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProyectoPreventasProSis
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnListaClientes_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }

        private async void btnListaUsuarios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }

        private async void btnListaProductos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }

        private async void btnListaPedidos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Productos());
        }
    }
}
