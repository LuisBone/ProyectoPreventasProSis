using System;
using System.Collections.Generic;
using System.Linq;
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
        public DetalleUsuarios(int id, string usuario, string clave, string nombre, string telefono, string correo, int activo, string titulo)
        {
            InitializeComponent();
        }
    }
}