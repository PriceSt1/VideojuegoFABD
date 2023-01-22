using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideojuegoFABD.Models
{
    public class TLineaFactura
    {

        public string CodFactura { get; set; }
        public string Videojuego { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }

        public TLineaFactura(string codFactura, string videojuego, string cantidad, string total)
        {
            CodFactura = codFactura;
            Videojuego = videojuego;
            Cantidad = cantidad;
            Total = total;
        }
        public TLineaFactura()
        {

        }

        public override string ToString()
        {
            return Videojuego+" "+Cantidad+" "+Total;
        }
        public override bool Equals(object obj)
        {
            return ((TLineaFactura)obj).Videojuego == Videojuego;
        }
    }
}
