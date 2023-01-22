using VideojuegoFABD.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideojuegoFABD.Models
{
    public class TCliente
    {
   
        public string CodCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string DNI { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Borrado { get; set; }

        public TCliente(string nombre, string apellidos, string dNI, string direccion, string email)
        {   CodCliente= Util.GenerarCodigo(this.GetType());
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dNI;
            Direccion = direccion;
            Email = email;
            Borrado = "0";
        }

        public TCliente(string codCliente, string nombre, string apellidos, string dNI, string direccion, string email,string borrado)
        {
            CodCliente = codCliente;
            Nombre = nombre;
            Apellidos = apellidos;
            DNI = dNI;
            Direccion = direccion;
            Email = email;
            Borrado = borrado;
        }

        public TCliente()
        {

        }

        public override string ToString()
        {
            return CodCliente.ToLower()+": "+Nombre.ToUpper()+" "+Apellidos.ToUpper();
        }
    }
}
