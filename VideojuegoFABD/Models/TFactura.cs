using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideojuegoFABD.Models
{
    class TFactura
    {
      

        public string CodFactura { get; set; }
        public string Cliente { get; set; }
        public string FechaFactura { get; set; }
        public string Borrado { get; set; }

        public TFactura(string codFactura, string cliente, string fechaFactura, string borrado)
        {
            CodFactura = codFactura;
            Cliente = cliente;
            FechaFactura = fechaFactura;
            Borrado = borrado;
        }

        public TFactura(string codFactura,string cliente, string fechaFactura)
        {
            CodFactura = codFactura;
            Cliente = cliente;
            FechaFactura = fechaFactura;
            Borrado = "0";
        }
        public TFactura() { }

        public override string ToString()
        {
            return CodFactura.ToLower()+":"+Cliente.ToUpper();
        }
       
    }
}
