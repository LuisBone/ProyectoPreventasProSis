using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPreventasProSis.Ws
{
    public class ItemUsuario
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public int activo { get; set; }
    }

    public class InfoUsuario
    {
        public List<ItemUsuario> items { get; set; }
        public int total { get; set; }
    }

    public class RootUsuario
    {
        public string mensaje { get; set; }
        public InfoUsuario info { get; set; }
    }
}
