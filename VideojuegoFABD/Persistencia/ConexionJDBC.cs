using System;
using System.Configuration;
using MySqlConnector;

namespace VideojuegoFABD.Persistencia
{
    public class ConexionJDBC
    {
        private static MySqlConnection connection;

        /*  
         *  Abre la conexion con la base de datos
         */
        public static MySqlConnection AbrirConexion()
        {
            if (connection == null)
            {
                try
                {
                    connection = new MySqlConnection();
                    connection.ConnectionString =
                        "Server=" + ConfigurationManager.AppSettings["servidor"].ToString()
                        + ";Database=" + ConfigurationManager.AppSettings["baseDatos"].ToString()
                        + ";Uid=" + ConfigurationManager.AppSettings["usuario"].ToString()
                        + ";Pwd=" + ConfigurationManager.AppSettings["password"].ToString() + "; Connection LifeTime = 3600;" +
                        "Max Pool Size=100;Min Pool Size=1;Pooling=true;";
                    connection.Open();


                }
                catch (Exception ex)
                {
                    string Success = ex.Message;
                    throw new Exception(ex.Message, ex);
                }
            }
            
            return connection;
        }

        public static void CerrarConexion()
        {
            if(connection!=null) connection.Close();
            
        }
    }
}
