using System;
using System.Collections.Generic;
using System.Text;

namespace VideojuegoFABD.Persistencia
{
    public class UtilSQL
    {
        private static StringBuilder sql = new StringBuilder();

        public static string SqlInsertar(object objeto)
        {
            sql.Clear();
            sql.Append("INSERT INTO " + objeto.GetType().Name.ToLower() + " ( ");
            RellenarSql(AccesoBD.ObtenerNombrePropiedades(objeto.GetType()));
            return sql.ToString();
        }

        public static string SqlBuscar(Type clase)
        {
            sql.Clear();
            sql.Append("SELECT * FROM " + clase.Name.ToLower() + " WHERE " + obtenerClave(clase) + " = @a1 ");
            return sql.ToString();
        }

        public static string SqlBorrar(Object objeto)
        {
            sql.Clear();
            sql.Append("DELETE FROM " + objeto.GetType().Name.ToLower() + " WHERE " + obtenerClave(objeto.GetType()) + " = @1 ");
            return sql.ToString();
        }

        public static string SqlModificar(Object objeto)
        {
            sql.Clear();
            sql.Append("UPDATE " + objeto.GetType().Name.ToLower() + " SET ");
            int index = 1;
            foreach (var item in AccesoBD.ObtenerNombrePropiedades(objeto.GetType()))
            {
                sql.Append(item+" = @" + (index++) + " , ");
            }
            sql.Remove(sql.Length - 2, 2);
            sql.Append("WHERE " + obtenerClave(objeto.GetType()) + " = @" + index);
            return sql.ToString();
        }

        public static string SqlObtener(Type clase)
        {
            sql.Clear();
            sql.Append("SELECT * FROM " + clase.Name.ToLower() + " WHERE Borrado LIKE 0");
            return sql.ToString();
        }


        private static string obtenerClave(Type clase)
        {
            foreach (var item in AccesoBD.ObtenerNombrePropiedades(clase))
            {
                if (item.StartsWith("Cod"))
                {
                    return item;
                }
            }
            return null;
        }

        private static void RellenarSql(List<string> list)
        {
            StringBuilder cadena = new StringBuilder(" ) VALUES ( ");
            int index = 1;
            foreach (string item in list)
            {
                sql.Append(item + ", ");
                cadena.Append("@" + (index++) + " , ");
            }
            sql.Remove(sql.Length - 2, 2);
            sql.Append(cadena.Remove(cadena.Length - 2, 2) + " ) ");
        }







    }
}
