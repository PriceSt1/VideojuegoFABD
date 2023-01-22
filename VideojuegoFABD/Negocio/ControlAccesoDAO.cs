using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideojuegoFABD.Persistencia;

namespace VideojuegoFABD.Negocio
{
    public class ControlAccesoDAO<T> : IAcceso<T> where T : new()
    {
        AccesoDAO<T> accesoDAO = new AccesoDAO<T>();

        public bool BorradoVirtual(object objeto)
        {
            return ((IAcceso<T>)accesoDAO).BorradoVirtual(objeto);
        }

        public bool Borrar(object objeto)
        {
            return ((IAcceso<T>)accesoDAO).Borrar(objeto);
        }

        public object Buscar(Type clase, string nombre)
        {
            return ((IAcceso<T>)accesoDAO).Buscar(clase, nombre);
        }

        public List<object> Buscar(Type clase, string campo, string busqueda)
        {
            return ((IAcceso<T>)accesoDAO).Buscar(clase, campo, busqueda);
        }

        public bool Insertar(T objeto)
        {
            return ((IAcceso<T>)accesoDAO).Insertar(objeto);
        }

        public bool Modificar(string clavePrimaria, T objeto)
        {
            return ((IAcceso<T>)accesoDAO).Modificar(clavePrimaria, objeto);
        }

        public List<object> Obtener(Type clase)
        {
            return ((IAcceso<T>)accesoDAO).Obtener(clase);
        }
    }
}