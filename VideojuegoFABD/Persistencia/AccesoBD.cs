using System;
using System.Collections.Generic;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace VideojuegoFABD.Persistencia
{
    public class AccesoBD
    {
        private static MySqlConnection connection = null;
        private MySqlTransaction transaccion;
        private MySqlCommand comando;

        public AccesoBD()
        {
            try
            {
                connection = ConexionJDBC.AbrirConexion();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message, e);
            }


        }

        public bool Insertar(string sql, object objecto, string antiguo)
        {
            try
            {
                //"SELECT * FROM admin WHERE admin_username=@val1 AND admin_password=PASSWORD(@val2)"
                comando = new MySqlCommand(sql, connection); ///Esto es como preparedStatement
                Dictionary<string, object> map = ObtenerDictionaryValorPropiedades(objecto);
                int index = 1;
                foreach (string valor in map.Values)
                {
                    comando.Parameters.AddWithValue("@" + index++, valor);
                }
                if (sql.Contains("UPDATE"))
                {
                    comando.Parameters.AddWithValue("@" + index, antiguo);
                }

            }
            catch (Exception) { throw; }

            return comando.ExecuteNonQuery() > 0;

        }
        public bool Borrar(string sql, object objeto)
        {
            try
            {
                comando = new MySqlCommand(sql, connection);
                comando.Parameters.AddWithValue("@1", ObtenerValorClavePrimaria(objeto));
            }
            catch (Exception)
            {
                throw;
            }
            return comando.ExecuteNonQuery() > 0;
        }

        public List<object> Consultar(string sql, Type clase, string nombre)
        {
            List<object> objetos = null;
            MySqlDataReader sqlDataReader = null;
            try
            {
                comando = new MySqlCommand(sql, connection);
                if (!nombre.Equals(""))
                {
                    comando.Parameters.AddWithValue("@a1", nombre);
                }
                sqlDataReader = comando.ExecuteReader();
                if (sqlDataReader != null)
                {
                    objetos = new List<object>();
                    List<string> list = ObtenerNombrePropiedades(clase);
                    {
                        while (sqlDataReader.Read())
                        {
                            object obj = Activator.CreateInstance(clase);
                            foreach (string name in list)
                            {
                                string valor = (String)sqlDataReader[name].ToString();
                                PropertyInfo propiedad = obj.GetType().GetProperty(name);
                                propiedad.SetValue(obj, Convert.ChangeType(valor, propiedad.PropertyType), null);

                            }
                            objetos.Add(obj);
                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                sqlDataReader.Close();
            }
            return objetos;
        }

        public static string ObtenerValorClavePrimaria(object objeto)
        {
            foreach (PropertyInfo propiedad in objeto.GetType().GetProperties())
            {
                if (propiedad.Name.StartsWith("Cod"))
                {
                    return Convert.ToString(propiedad.GetValue(objeto, null));
                }
            }
            return null;
        }

        /*
       * Obtiene un diccionario con el nombre de la propiedad y el valor de ella
       */
        protected static Dictionary<string, object> ObtenerDictionaryValorPropiedades(object objeto)
        {
            Dictionary<string, object> mapM = new Dictionary<string, object>();
            foreach (PropertyInfo method in objeto.GetType().GetProperties())
            {
                mapM.Add(method.Name, method.GetValue(objeto, null));
            }
            return mapM;
        }
        /*
       * Se obtiene una lista con el nombre de las propiedades para, posteriormente, hacer el set
       */
        public static List<string> ObtenerNombrePropiedades(Type clase)
        {
            List<string> lista = new List<string>();
            //Recorremos las propiedades y almacenamos el nombre
            foreach (PropertyInfo propiedad in clase.GetProperties())
            {
                lista.Add(propiedad.Name);
            }
            return lista;
        }

        public string ObtenerCodigo(Type clase)
        {
            string sql = "SELECT MAX(Cod" + clase.Name.Substring(1) + ") FROM " + clase.Name.ToLower();
            MySqlDataReader sqlDataReader = null;
            try
            {
                comando = new MySqlCommand(sql, connection);
                sqlDataReader = comando.ExecuteReader();
                sqlDataReader.Read();
                return sqlDataReader[0].ToString();
            }
            catch (Exception) { return null; }
            finally
            {
                sqlDataReader.Close();
            }
        }

        public void StartTransaction()
        {
            transaccion = connection.BeginTransaction();
        }

        public void RollBack()
        {
            transaccion.Rollback();
        }

        public void Commit()
        {
            transaccion.Commit();
        }

        public void CloseConnection()
        {
            connection.Close();
        }
    }
}
