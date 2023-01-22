using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace VideojuegoFABD.Persistencia
{
    public static class Util
    {
        private static string ruta = AppDomain.CurrentDomain.BaseDirectory + "sql.txt";
        private static Dictionary<string, string> SENTENCIAS;
        static BinaryFormatter serializer = new BinaryFormatter();


        public static string GuardarSQL(string orden, string sql)
        {
            SENTENCIAS.Add(orden, sql);
            return sql;
        }

        public static string ExisteSentencia(string orden)
        {
            foreach (var item in SENTENCIAS)
            {
                if (item.Key.Equals(orden))
                {
                    return item.Value;
                }
            }
            return null;
        }



        public static void RellenarDictionarySentencias()
        {

            if (ComprobarArchivo() && File.ReadAllLines(ruta).Count() > 0)
            {
                using (var stream = File.OpenRead(ruta))
                {
                    SENTENCIAS = (Dictionary<string, string>)serializer.Deserialize(stream);
                    stream.Close();
                }

            }
            else
            {
                SENTENCIAS = new Dictionary<string, string>();
            }
        }

        public static void EscribirDictionarySentenciasFichero()
        {
            if (ComprobarArchivo() && SENTENCIAS != null)
            {
                using (var stream = File.OpenWrite(ruta))
                {
                    serializer.Serialize(stream, SENTENCIAS);
                    stream.Close();
                }
            }
        }

        private static bool ComprobarArchivo()
        {
            if (!File.Exists(ruta))
            {
                try
                {
                    File.Create(ruta).Close(); ;
                }
                catch
                {
                    //Errores.controlError(new Errores(Errores.ERROR_FICHERO));
                    return false;
                }
            }
            return true;
        }
        public static string GenerarCodigo(Type clase)
        {
            string codigo = new AccesoBD().ObtenerCodigo(clase);
            if (codigo.Equals(""))
            {
                return "cod001";
            }
            int indice = int.Parse(codigo.Substring(3)) + 1;
            if (indice >= 10)
                return "cod0" + indice;
            else if (indice >= 100)
                return "cod" + indice;
            else
                return "cod00" + indice;
        }


    }
}
