using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPreventasProSis.Ws
{
    public class ItemCliente
    {

        //cliente 
        //funciono 

        public int id { get; set; }
        public string identificacion { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string direccion { get; set; }
        public string pais { get; set; }
        public string ciudad { get; set; }
        public int activo { get; set; }
    }

    public class InfoCliente
    {
        public List<ItemCliente> items { get; set; }
        public int total { get; set; }
    }

    public class RootCliente
    {
        public string mensaje { get; set; }
        public InfoCliente info { get; set; }
    }


}
