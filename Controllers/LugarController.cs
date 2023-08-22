using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurismoMexicoMVC.Models;
using TurismoMexicoMVC.Models.ViewModels;
using static TurismoMexicoMVC.Models.Enum;

namespace TurismoMexicoMVC.Controllers
{
    public class LugarController : Controller
    {
        // GET: Lugar
        public ActionResult Index()
        {
            List<LugaresLista> lista = new List<LugaresLista>();
            using (TurismoMexicoEntities1 context = new TurismoMexicoEntities1())
            {
                lista = (from l in context.lugares
                         join cat in context.categorias on l.categoria_id equals cat.id_categoria
                         select new LugaresLista
                         {
                             id_lugar = l.id_lugar,
                             nombre=l.nombre,
                             descripcion=l.descripcion,
                             ubicacion=l.descripcion,
                             categoria_id = cat.id_categoria,
                             nombre_cat=cat.nombre,
                         }
                ).ToList();
            }
            return View(lista);
        }

        public ActionResult Editar_Lugar(int id)
        {
            //llamamos al contexto, voy a trabajar con el contexto
            lugares lugares = new lugares();
            using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
            {
                lugares = db.lugares.Where(x => x.id_lugar == id).FirstOrDefault();
            }
            ViewBag.Title = "Editar Lugar n° " + lugares.id_lugar;
            CargarDDL();
            return View(lugares);
        }

        //Polimorfismo de los métodos de arriba y abajo, enel método de abajo regresamos la peticion de POST, arriba solo es GET
        [HttpPost]
        public ActionResult Editar_Lugar(lugares model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                    {
                        var lugares = new lugares();
                        lugares.id_lugar= model.id_lugar;
                        lugares.nombre = model.nombre;
                        lugares.descripcion = model.descripcion;
                        lugares.ubicacion = model.ubicacion;
                        lugares.categoria_id = model.categoria_id;
                        db.Entry(lugares).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        CargarDDL();
                        Alert("Actualización exitosa", NotificationType.success);
                    }
                    return Redirect("~/Lugar");
                }
                Alert("Verificar la información", NotificationType.warning);
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Ha ocurrido un error: " + ex.Message, NotificationType.error);
                CargarDDL();
                return View(model);
            }
        }



        public ActionResult Nuevo_Lugar()
        {
            CargarDDL();
            return View();
        }
        [HttpPost]
        //Polimorfismo de los métodos de arriba y abajo, enel método de abajo regresamos la peticion de POST, arriba solo es GET
        public ActionResult Nuevo_Lugar(NuevoLugar model)
        {
            try
            {
                //validamos si el modelo es válido: esto es un helper para validar datos
                //modelstate es por parte de razor
                if (ModelState.IsValid)
                {
                    //entro a mi contexto de la base de datos
                    using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                    {

                        var lugar = new lugares();
                        lugar.id_lugar = model.id_lugar;
                        lugar.nombre = model.nombre;
                        lugar.descripcion = model.descripcion;
                        lugar.ubicacion = model.ubicacion;
                        lugar.categoria_id = model.categoria_id;

                        //dentro del mi contexto, en el contexto de camiones se guardan los datos
                        db.lugares.Add(lugar);
                        db.SaveChanges();
                        CargarDDL();
                        Alert("Registro guardado con éxito", NotificationType.success);
                    }

                    //si lo anterior se hizo con éxito regreso a la vista de Camiones
                    return Redirect("~/Lugar");
                }
                //SI REDIRECCIONO AL VIEW MODEL ES PORQUE FALLÓ UNA VALIDACIÓN
                Alert("Verificar la información", NotificationType.warning);
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Alert("Ha ocurrido un error: " + ex.Message, NotificationType.error);
                CargarDDL();
                return View(model);
            }
        }

        //DELETE
        [HttpGet]
        public ActionResult Eliminar_Lugar(int id)
        {
            lugares lugar = new lugares();
            try
            {
                using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                {
                    lugar = db.lugares.Where(x => x.id_lugar == id).FirstOrDefault();
                    db.lugares.Remove(lugar);
                    db.SaveChanges();
                }
                Alert("Eliminación exitosa", NotificationType.success);
                return Redirect("~/Lugar");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NotificationType.error);
                return Redirect("~/Lugar");
            }
        }

        public void CargarDDL()
        {
            //aqui agregamos los ddl : crear primero el modelo CamionesDDL
            List<CategoriaDDL> listacat = new List<CategoriaDDL>();
            listacat.Insert(0, new CategoriaDDL { id_categoria = 0, nombre_cat = "Seleccione una categoria" });

            using (TurismoMexicoEntities1 context = new TurismoMexicoEntities1())
            {

                foreach (var c in context.categorias)
                {
                    //por cada camion se crea un auxiliar y se añade a la lista
                    CategoriaDDL aux = new CategoriaDDL();
                    aux.id_categoria = c.id_categoria;
                    aux.nombre_cat = c.nombre;
                    listacat.Add(aux);
                }
            }
            ViewBag.ListaCategoria = listacat;
        }

        public void Alert(string message, NotificationType notificacionType)
        {
            var msg = "<script language='javascript'>Swal.fire('" +
                notificacionType.ToString().ToUpper() + "','" + message + "','" +
                notificacionType + "')" + "</script>";

            //asignamos la propiedad notification y el mensaje
            TempData["notification"] = msg;
        }
    }
}