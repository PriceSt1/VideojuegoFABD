using VideojuegoFABD.Persistencia;


namespace VideojuegoFABD.Models
{
    public class TLinea
    {
        public string CodFactura { get; set; }
        public string Videojuego { get; set; }
        public string Cantidad { get; set; }
        public string Total { get; set; }
        

        public TLinea(string codLibro, string videojuego, string titulo, string tema, string paginas, string precio, string formatouno, string formatodos, string formatotres, string estado, string borrado)
        {   this.CodFactura = codLibro;
            this.Videojuego = videojuego;
            this.Cantidad = titulo;
            this.Total = tema;
           
        }
        public TLinea(string videojuego, string titulo, string tema, string paginas, string precio, string formatouno, string formatodos, string formatotres, string estado)
        {   
            this.CodFactura =Util.GenerarCodigo(this.GetType());
            this.Videojuego = videojuego;
            this.Cantidad = titulo;
            this.Total = tema;
        }
        public TLinea() { }

        public override string ToString()
        {
            return CodFactura+": " +Videojuego.ToUpper();
        }

    }

    

}
