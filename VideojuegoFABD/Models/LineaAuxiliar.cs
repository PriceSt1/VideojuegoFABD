using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideojuegoFABD.Models
{
    public class LineaAuxiliar
    {
        public string CodFactura { get; set; }
        public TVideojuego Videojuego { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }

        public LineaAuxiliar(string codFactura, TVideojuego videojuego, string cantidad, string total)
        {
            CodFactura = codFactura;
            Videojuego = videojuego;
            Cantidad = cantidad;
            Total = total;
        }
        public LineaAuxiliar()
        {

        }

        public double subTotal()
        {
            return double.Parse(Total.Replace('.', ','));
        }

    }
}