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
    public class VideojuegoController : Controller
    {
        private ControlAccesoDAO<TVideojuego> control = new ControlAccesoDAO<TVideojuego>();
        //***********************************************************************************************
        // Opciones del menú :    Consultar
        //                        Añadir Libro
        //                        Carro Compra
        //***********************************************************************************************
        public ActionResult Consultar()
        {
            List<TVideojuego> list = new List<TVideojuego>();

            foreach (var item in control.Obtener(new TVideojuego().GetType()))
            {
                list.Add((TVideojuego)item);
            }

            return View(list);
        }

        public PartialViewResult borrarVideojuego(string CodeVideojuego)
        {
            control.BorradoVirtual((TVideojuego)control.Buscar(new TVideojuego().GetType(), CodeVideojuego));
            object[] modelos = new object[1];
            //Obtenemos los tipos de libros
            modelos[0] = control.Obtener(new TVideojuego().GetType());
            return PartialView("_BorrarVideojuego", modelos);
        }
        public ActionResult verVideojuego(string CodVideojuego)
        {
            return PartialView("_verVideojuego", (TVideojuego)control.Buscar(new TVideojuego().GetType(), CodVideojuego));
        }
        // Creamos el metodo addLibro que manda a la vista una lista de objetos genericos, en este caso necesitamos la lista de temas-
        public ActionResult addVideojuego()
        {   //Obtenemos la lisa de temas
            return View(control.Obtener(new TGenero().GetType()));
        }

        //Aqui repetimos el mismo metodo, pero le entra un Libro.
        //Esto es una anotacion, dice que es un metodo post. 
        [HttpPost]
        public ActionResult addLibro(TVideojuego videojuego)
        {
            try
            {
                List<object> libros = new List<object>();
                //Hacemos la operaciones necesarias para guardar el nuevo libro.
                videojuego.Precio = videojuego.Precio.Replace(".", ",");
                videojuego.CodVideojuego = Util.GenerarCodigo(videojuego.GetType());
                videojuego.Borrado = "0";
                //
                libros.Add((TVideojuego)videojuego);
                if (control.Insertar(libros))
                {
                    return Json("Libro insertado correctamente");
                }

            }
            catch (Exception e)
            {
                return Json(Errores.ControlErrores(e));
            }
            return RedirectToAction("addLibro");
        }

        public ActionResult modificarVideojuego(string codVideojuego)
        {
            //Creamos el array de objetos de 2 posiciones, en el guardaremos el libro y la lista de Temas.
            object[] modelos = new object[2];
            //Obtenermos el libro en a editar en concreto.
            modelos[0] = control.Buscar(new TVideojuego().GetType(), codVideojuego);
            //Obtenemos los tipos de libros
            modelos[1] = control.Obtener(new TGenero().GetType());
            //Devolvemos el array a la vista.
            return View(modelos);
        }

        [HttpPost]
        public ActionResult modificarVideojuego(TVideojuego videojuego)
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
                return Content(Mensaje.mostrarmensaje(Errores.ControlErrores(e), "modificarLibro"));
            }
        }
        //
        //  Métodos utilizados para gestionar el carro de la compra
        //
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
        public ActionResult obtenerVideojuego(string CodLibro)
        {
            object[] modelos = new object[1];
            modelos[0] = control.Buscar(new TVideojuego().GetType(), CodLibro);
            //Obtenemos los tipos de libros
            return Json(modelos);
        }

        [HttpPost]
        public ActionResult comprar(List<TLinea> data)
        {
            //******************** Grabar objeto FACTURA *************************************************
            TFactura factura = new TFactura("cod022", "admin", DateTime.Now.ToShortDateString());
            List<object> listaFacturaTemp = new List<object>();
            listaFacturaTemp.Add(factura);
            //control.Insertar(listaFacturaTemp);
            //******************** Grabar las Líneas de Factura.
            List<object> listaLineasFactura = new List<object>();

            foreach (TLinea linea in data)
            {
                TLineaFactura lineaTemp = new TLineaFactura(factura.CodFactura, linea.Videojuego, linea.Cantidad.ToString(), linea.Total.ToString());
                listaLineasFactura.Add(lineaTemp);
            }

            //control.Insertar(listaLineasFactura);
            //********************
            return Json("Factura guardada correctamente");
        }
    }
}
