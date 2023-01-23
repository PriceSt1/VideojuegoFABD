using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideojuegoFABD.Comun;
using VideojuegoFABD.Models;
using VideojuegoFABD.Negocio;
using VideojuegoFABD.Persistencia;

namespace VideojuegoFABD.Controllers
{
    public class InicioController : Controller
    {
        ControlAccesoDAO<TUsuario> control = new ControlAccesoDAO<TUsuario>();

        public ActionResult Inicio()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(TUsuario usuario)
        {
            TUsuario usuTemp = control.Buscar(usuario.GetType(), "Nick", usuario.Nick).Count == 0 ? null : (TUsuario)control.Buscar(usuario.GetType(), "Nick", usuario.Nick).First();
            if (usuTemp != null)
            {
                if (ComprobarUsuario(usuario, usuTemp))
                {
                    usuTemp.Pass = null;
                    Session.Add("usuario", usuTemp);
                    return View("Inicio");
                }
                else
                {
                    return Content(Mensaje.mostrarmensaje("Contraseña Incorrecta...", "Inicio"));
                }
            }
            else
            {
                return Content(Mensaje.mostrarmensaje("Usuario no encontrado..., registrese", "Inicio"));
            }
        }

        [HttpPost]
        public ActionResult Registro(TUsuario usuario)
        {
            if (control.Buscar(usuario.GetType(), "Nick", usuario.Nick).Count == 0)
            {
                usuario.CodUsuario = Util.GenerarCodigo(usuario.GetType());
                usuario.Rol = "user";

                List<object> listaUsu = new List<object>();
                listaUsu.Add(usuario);

                if (control.Insertar(listaUsu))
                {
                    Session["usuario"] = usuario;
                    return View("Inicio");
                }
                else
                {
                    return Content(Mensaje.mostrarmensaje("Fallo a la hora de guardar", "Inicio"));
                }
            }
            return Content(Mensaje.mostrarmensaje("El usuario ya existe", "Inicio"));
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return View("Inicio");
        }

        private bool addUsuario(TUsuario usuario)
        {

            return true;
        }

        private bool ComprobarUsuario(TUsuario usuario, TUsuario temporal)
        {

            if (usuario.Rol == null && temporal != null)
            {
                if (usuario.Pass.Equals(temporal.Pass))
                {
                    return true;
                }
            }
            else if (usuario.Rol != null && temporal == null)
            {
                return true;
            }
            return false;
        }

    }
}