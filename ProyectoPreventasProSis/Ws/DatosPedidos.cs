using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPreventasProSis.Ws
{
    public class ItemPedido
    {
        public int id { get; set; }
        public int cliente_id { get; set; }
        public string fecha { get; set; }
        public int usuario_id { get; set; }
        public int estado { get; set; }
        public string cliente { get; set; }
        public int n_productos { get; set; }
        public string vendedor { get; set; }
        public double total { get; set; }
    }

    public class InfoPedido
    {
        public List<ItemPedido> items { get; set; }
        public int total { get; set; }
    }

    public class RootPedido
    {
        public string mensaje { get; set; }
        public InfoPedido info { get; set; }
    }

}
