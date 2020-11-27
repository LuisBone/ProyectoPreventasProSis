using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoPreventasProSis.Ws
{
    public class DatosCabeceraPedido
    {
        public int cliente_id { get; set; }
        public int usuario_id { get; set; }
        public int estado { get; set; }
    }
    public class DatosLista
    {
        public int id;
        public Ws.Item producto;
        public int cantidad;
        public double precio;
    }
}
