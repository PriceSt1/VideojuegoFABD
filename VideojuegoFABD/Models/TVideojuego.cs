using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideojuegoFABD.Persistencia;

namespace VideojuegoFABD.Models
{
    public class TVideojuego
    {
        public string CodVideojuego { get; set; }
        public string Titulo { get; set; }
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

        public TVideojuego(string codVideojuego, string titulo, string desarrollador, string editor, string genero, string precio, string fechaLanzamiento, string idioma, string pegi, string borrado, string portada, string trailer)
        {
            CodVideojuego = codVideojuego;
            Titulo = titulo;
            Desarrollador = desarrollador;
            Editor = editor;
            Genero = genero;
            Precio = precio;
            FechaLanzamiento = fechaLanzamiento;
            Idioma = idioma;
            Pegi = pegi;
            Portada = portada;
            Trailer = trailer;
            Borrado = borrado;
        }

        public TVideojuego(string titulo, string desarrollador, string editor, string genero, string precio, string fechaLanzamiento, string idioma, string pegi, string borrado, string portada, string trailer)
        {
            CodVideojuego = Util.GenerarCodigo(this.GetType());
            Titulo = titulo;
            Desarrollador = desarrollador;
            Editor = editor;
            Genero = genero;
            Precio = precio;
            FechaLanzamiento = fechaLanzamiento;
            Idioma = idioma;
            Pegi = pegi;
            Portada = portada;
            Trailer = trailer;
            Borrado = "0";
        }

        public TVideojuego()
        {
        }

        public override string ToString()
        {
            return CodVideojuego + ": " + Titulo.ToUpper();
        }
    }
}