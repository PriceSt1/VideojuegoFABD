using System;
using System.Collections.Generic;
using System.Linq;

namespace VideojuegoFABD.Persistencia
{
    public class AccesoDAO<T> : IAcceso<T> where T : new()
    {
        public bool BorradoVirtual(object objeto)
        {
            AccesoBD acceso = new AccesoBD();
            acceso.StartTransaction();
            string sql;
            foreach (var item in objeto.GetType().GetProperties())
            {
                if (item.Name.Contains("Borra"))
                {
                    item.SetValue(objeto, "1");
                }
            }
            if ((sql = Util.ExisteSentencia("BORRADOVIRTUAL" + objeto.GetType().Name)) == null)
            {
                if (acceso.Insertar(Util.GuardarSQL("BORRADOVIRTUAL" + objeto.GetType().Name, UtilSQL.SqlModificar(objeto)), objeto, AccesoBD.ObtenerValorClavePrimaria(objeto)))
                {
                    acceso.Commit();
                    return true;
                }
            }
            else
            {
                if (acceso.Insertar(sql, objeto, AccesoBD.ObtenerValorClavePrimaria(objeto)))
                {
                    acceso.Commit();
                    return true;
                }
            }
            acceso.RollBack();
            return false;
        }

        public bool Borrar(object objeto)
        {
            AccesoBD acceso = new AccesoBD();
            acceso.StartTransaction();
            string sql;
            if ((sql = Util.ExisteSentencia("DELETE" + objeto.GetType().Name)) == null)
            {
                if (acceso.Borrar(Util.GuardarSQL("DELETE" + objeto.GetType().Name, UtilSQL.SqlBorrar(objeto)), objeto))
                {
                    acceso.Commit();
                    return true;
                }
            }
            else
            {
                if (acceso.Borrar(sql, objeto))
                {
                    acceso.Commit();
                    return true;
                }
            }
            acceso.RollBack();
            return false;
        }

        public object Buscar(Type clase, string nombre)
        {
            List<object> list = null;
            AccesoBD accesoBD = new AccesoBD();
            String sql;
            if ((sql = Util.ExisteSentencia("SELECTONE" + clase.Name)) == null)
            {
                if ((list = accesoBD.Consultar(Util.GuardarSQL("SELECTONE" + clase.Name, UtilSQL.SqlBuscar(clase)), clase, nombre)).Count == 0)
                {
                    return null;
                }
            }
            else
            {
                if ((list = accesoBD.Consultar(sql, clase, nombre)).Count == 0)
                {
                    return null;
                }
            }
            return list.First();
        }

        public List<object> Buscar(Type clase, string campo, string busqueda)
        {
            AccesoBD acceso = new AccesoBD();
            String sql = "SELECT * FROM " + clase.Name + " WHERE " + campo + " = \"" + busqueda + "\"";
            return acceso.Consultar(sql, clase, "");
        }


        public bool Insertar(List<object> list)
        {
            AccesoBD acceso = new AccesoBD();
            string sql;
            try
            {
                acceso.StartTransaction();
                foreach (var obj in list)
                {
                    if ((sql = Util.ExisteSentencia("INSERTAR" + obj.GetType().Name)) == null)
                    {
                        acceso.Insertar(
                            Util.GuardarSQL("INSERTAR" + obj.GetType().Name, UtilSQL.SqlInsertar(obj)), obj, "");
                    }
                    else
                    {
                        acceso.Insertar(sql, obj, "");

                    }
                }
                acceso.Commit();
                return true;
            }
            catch (Exception)
            {
                acceso.RollBack();
                throw;
            }
        }


        public bool Modificar(string nombre, T objeto)
        {
            AccesoBD acceso = new AccesoBD();
            acceso.StartTransaction();
            string sql;
            if ((sql = Util.ExisteSentencia("UPDATE" + objeto.GetType().Name)) == null)
            {
                if (acceso.Insertar(Util.GuardarSQL("UPDATE" + objeto.GetType().Name, UtilSQL.SqlModificar(objeto)),
                    objeto, nombre))
                {
                    acceso.Commit();
                    return true;
                }
            }
            else
            {
                if (acceso.Insertar(sql, objeto, nombre))
                {
                    acceso.Commit();
                    return true;
                }
            }
            acceso.RollBack();
            return false;
        }

        public List<Object> Obtener(Type clase)
        {
            AccesoBD acceso = new AccesoBD();
            string sql;
            if ((sql = Util.ExisteSentencia("SELECTALL" + clase.Name)) == null)
            {
                return (acceso.Consultar(Util.GuardarSQL("SELECTALL" + clase.Name, UtilSQL.SqlObtener(clase)), clase, ""));
            }
            else
            {
                return acceso.Consultar(sql, clase, "");
            }

        }




    }
}
