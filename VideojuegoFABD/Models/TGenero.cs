using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideojuegoFABD.Models
{
    public class TGenero
    {
        public string Genero { get; set; }

        public TGenero()
        {
        }

        public TGenero(string genero)
        {
            this.Genero = genero;
        }
    }
}