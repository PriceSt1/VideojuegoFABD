using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideojuegoFABD.Models
{
    public class TIdioma
    {
        public string Idioma { get; set; }

        public TIdioma()
        {
        }

        public TIdioma(string idioma)
        {
            Idioma = idioma;
        }
    }
}