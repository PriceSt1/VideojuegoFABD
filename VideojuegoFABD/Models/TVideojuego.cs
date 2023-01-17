using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideojuegoFABD.Models
{
    public class TVideojuego
    {
        public string CodVideojuego { get; set; }
        public string Desarrollador { get; set; }
        public string Editor { get; set; }
        public string Genero { get; set; }
        public string Precio { get; set; }
        public string FechaLanzamiento { get; set; }
        public string Idioma { get; set; }
        public string Pegi { get; set; }
        public string Borrado { get; set; }
        public string Portada { get; set; }
        public string Trailer { get; set; }

    }
}