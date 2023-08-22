using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using TurismoMexicoMVC.Models;
using TurismoMexicoMVC.Models.ViewModels;
using static TurismoMexicoMVC.Models.Enum;

namespace TurismoMexicoMVC.Controllers
{
    public class CategoriaController : Controller
    {
        // GET: Categoria
        public ActionResult Index()
        {
            List<CategoriasLista> List = new List<CategoriasLista>();
            using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
            {
                List = (from c in db.categorias
                        select new CategoriasLista
                        {
                            id_categoria=c.id_categoria,
                            nombre=c.nombre,
                            descripcion=c.descripcion,
                        }
                      ).ToList();
            }
            //en el return pasamos el modelo, osea la lista
            return View(List);
        }
        public ActionResult Nueva_Categoria()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Nueva_Categoria(NuevaCategoria model)
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
                        var categoria = new categorias();
                        categoria.id_categoria = model.id_categoria;
                        categoria.nombre = model.nombre;
                        categoria.descripcion = model.descripcion;
                        //dentro del mi contexto, en el contexto de camiones se guardan los datos
                        db.categorias.Add(categoria);
                        db.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.success);
                    }

                    //si lo anterior se hizo con éxito regreso a la vista de Camiones
                    return Redirect("~/Categoria");
                }
                //SI REDIRECCIONO AL VIEW MODEL ES PORQUE FALLÓ UNA VALIDACIÓN
                Alert("Verificar la información", NotificationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Alert("Ha ocurrido un error: " + ex.Message, NotificationType.error);
                return View(model);
            }
        }


        public ActionResult Editar_Categoria(int id)
        {
            //llamamos al contexto, voy a trabajar con el contexto
            categorias categorias = new categorias();
            using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
            {
                categorias = db.categorias.Where(x => x.id_categoria == id).FirstOrDefault();
            }
            ViewBag.Title = "Editar Categoria n° " + categorias.id_categoria;
            return View(categorias);
        }

        //Polimorfismo de los métodos de arriba y abajo, enel método de abajo regresamos la peticion de POST, arriba solo es GET
        [HttpPost]
        public ActionResult Editar_Categoria(categorias model)
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
                        var categoria = new categorias();
                        categoria.id_categoria = model.id_categoria;
                        categoria.nombre = model.nombre;
                        categoria.descripcion = model.descripcion;
                        //dentro del mi contexto, en el contexto de camiones se guardan los datos
                        db.Entry(categoria).State = System.Data.Entity.EntityState.Modified;
                        //db.camiones.Add(camion);
                        db.SaveChanges();
                        Alert("Registro guardado con éxito", NotificationType.success);
                    }

                    //si lo anterior se hizo con éxito regreso a la vista de Camiones
                    return Redirect("~/Categoria");
                }
                //SI REDIRECCIONO AL VIEW MODEL ES PORQUE FALLÓ UNA VALIDACIÓN
                Alert("Verificar la información", NotificationType.warning);
                return View(model);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Alert("Ha ocurrido un error: " + ex.Message, NotificationType.error);
                return View(model);
            }
        }

        //DELETE
        [HttpGet]
        public ActionResult Eliminar_Categoria(int id)
        {
            categorias categoria = new categorias();
            try
            {
                using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                {
                    categoria = db.categorias.Where(x => x.id_categoria == id).FirstOrDefault();
                    db.categorias.Remove(categoria);
                    db.SaveChanges();
                }
                Alert("Registro exitoso", NotificationType.success);
                return Redirect("~/Categoria");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NotificationType.error);
                return Redirect("~/Categoria");
            }
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