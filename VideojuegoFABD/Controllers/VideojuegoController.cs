using System;
using System.Collections.Generic;
using System.Web.Mvc;
using VideojuegoFABD.Comun;
using VideojuegoFABD.Models;
using VideojuegoFABD.Negocio;
using VideojuegoFABD.Persistencia;

namespace VideojuegoFABD.Controllers
{
    public class VideojuegoController : Controller
    {
        private ControlAccesoDAO<TVideojuego> control = new ControlAccesoDAO<TVideojuego>();
        public ActionResult Consultar()
        {
            List<TVideojuego> list = new List<TVideojuego>();

            foreach (var item in control.Obtener(new TVideojuego().GetType()))
            {
                list.Add((TVideojuego)item);
            }

            return View(list);
        }
        public PartialViewResult borrarVideojuego(string CodVideojuego)
        {
            control.BorradoVirtual((TVideojuego)control.Buscar(new TVideojuego().GetType(), CodVideojuego));
            object[] modelos = new object[1];
            //Obtenemos los tipos de libros
            modelos[0] = control.Obtener(new TVideojuego().GetType());
            return PartialView("_BorrarVideojuego", modelos);
        }
        public ActionResult verVideojuego(string CodVideojuego)
        {
            return PartialView("_verVideojuego", (TVideojuego)control.Buscar(new TVideojuego().GetType(), CodVideojuego));
        }
        public ActionResult addVideojuego()
        { 
            return View(control.Obtener(new TGenero().GetType()));
        }
        [HttpPost]
        public ActionResult addVideojuego(TVideojuego videojuego)
        {
            try
            {
                List<object> videojuegos = new List<object>();
                //Hacemos la operaciones necesarias para guardar el nuevo libro.
                videojuego.Precio = videojuego.Precio.Replace(".", ",");
                videojuego.CodVideojuego = Util.GenerarCodigo(videojuego.GetType());
                videojuego.Borrado = "0";
                //
                videojuegos.Add((TVideojuego)videojuego);
                if (control.Insertar(videojuegos))
                {
                    return Json("Videojuego insertado correctamente");
                }

            }
            catch (Exception e)
            {
                return Json(Errores.ControlErrores(e));
            }
            return RedirectToAction("Inicio");
        }
        public ActionResult modificarVideojuego(string codVideojuego)
        {
            object[] modelos = new object[2];
            modelos[0] = control.Buscar(new TVideojuego().GetType(), codVideojuego);
            modelos[1] = control.Obtener(new TGenero().GetType());
            return View(modelos);
        }
        [HttpPost]
        public ActionResult modificarVideojuegos(TVideojuego videojuego)
        {
            try
            {
                videojuego.Precio = videojuego.Precio.Replace(".", ",");
                videojuego.Borrado = "0";
                control.Modificar(videojuego.CodVideojuego, videojuego);
                return RedirectToAction("Consultar");
            }
            catch (Exception e)
            {
                return Content(Mensaje.mostrarmensaje(Errores.ControlErrores(e), "modificarVideojuego"));
            }
        }

        [HttpPost]
        public ActionResult obtenerVideojuego(string CodVideojuego)
        {
            object[] modelos = new object[1];
            modelos[0] = control.Buscar(new TVideojuego().GetType(), CodVideojuego);
            return Json(modelos);
        }
        public ActionResult carroCompra()
        {
            List<TVideojuego> list = new List<TVideojuego>();

            foreach (var item in control.Obtener(new TVideojuego().GetType()))
            {
                list.Add((TVideojuego)item);
            }

            return View(list);

        }
        [HttpPost]
        public ActionResult comprar(List<TLinea> data)
        {
            TFactura factura = new TFactura("", ((TUsuario)Session["usuario"]).Nick, DateTime.Now.ToShortDateString());
            factura.CodFactura = Util.GenerarCodigo(factura.GetType());
            List<object> listaFacturaTemp = new List<object>();
            listaFacturaTemp.Add(factura);
            List<object> listaLineasFactura = new List<object>();

            foreach (TLinea linea in data)
            {
                TLineaFactura lineaTemp = new TLineaFactura(factura.CodFactura, linea.Videojuego, linea.Cantidad.ToString(), linea.Total.ToString());
                listaLineasFactura.Add(lineaTemp);
            }

            control.Insertar(listaLineasFactura);
            control.Insertar(listaFacturaTemp);
            return Json("Factura guardada correctamente");
        }
    }
}
