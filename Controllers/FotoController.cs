using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurismoMexicoMVC.Models.ViewModels;
using TurismoMexicoMVC.Models;
using static TurismoMexicoMVC.Models.Enum;
using TurismoMexicoMVC.Models.DDL;

namespace TurismoMexicoMVC.Controllers
{
    public class FotoController : Controller
    {
        // GET: Foto
        public ActionResult Index()
        {
            List<FotosLista> lista = new List<FotosLista>();
            using (TurismoMexicoEntities1 context = new TurismoMexicoEntities1())
            {
                lista = (from f in context.fotos
                         join lug in context.lugares on f.lugar_id equals lug.id_lugar
                         select new FotosLista
                         {
                             id_foto = f.id_foto,
                             lugar_id = f.lugar_id,
                             nombre_lugar = lug.nombre,
                             url_foto = f.url_foto,
                             descripcion = f.descripcion
                         }
                ).ToList();
            }
            return View(lista);
        }

        public ActionResult Nueva_Foto()
        {
            CargarDDL();
            return View();
        }

        [HttpPost]
        public ActionResult Nueva_Foto(NuevaFoto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                    {
                        var foto = new fotos();
                        foto.lugar_id = model.lugar_id;
                        foto.url_foto = model.url_foto;
                        foto.descripcion = model.descripcion;
                        db.fotos.Add(foto);
                        db.SaveChanges();
                        CargarDDL();
                        Alert("Registro exitoso", NotificationType.success);
                    }
                    return Redirect("~/Foto");
                }
                Alert("Verificar la información", NotificationType.warning);
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Verificar la información", NotificationType.error);
                CargarDDL();
                return View(model);
            }
        }

        public ActionResult Editar_Foto(int id)
        {
            //llamamos al contexto, voy a trabajar con el contexto
            fotos fotos = new fotos();
            using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
            {
                fotos = db.fotos.Where(x => x.id_foto == id).FirstOrDefault();
            }
            ViewBag.Title = "Editar foto # " + fotos.id_foto;
            CargarDDL();
            return View(fotos);
        }

        [HttpPost]
        public ActionResult Editar_Foto(fotos model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                    {
                        var fotos = new fotos();
                        fotos.id_foto = model.id_foto;
                        fotos.lugar_id = model.lugar_id;
                        fotos.url_foto = model.url_foto;
                        fotos.descripcion=model.descripcion;
                        db.Entry(fotos).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        CargarDDL();
                        Alert("Actualización exitosa", NotificationType.success);
                    }
                    return Redirect("~/Foto");
                }
                Alert("Ha ocurrido un error, inténtelo más tarde", NotificationType.warning);
                CargarDDL();
                return View(model);
            }
            catch (Exception ex)
            {
                Alert("Ha ocurrido un error, inténtelo más tarde", NotificationType.error);
                CargarDDL();
                return View(model);
            }
        }

        public ActionResult Eliminar_Foto(int id)
        {
            fotos foto = new fotos();
            try
            {
                using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                {
                    foto = db.fotos.Where(x => x.id_foto == id).FirstOrDefault();
                    db.fotos.Remove(foto);
                    db.SaveChanges();
                }
                Alert("Eliminación exitosa", NotificationType.success);
                return Redirect("~/Foto");
            }
            catch (Exception ex)
            {
                Alert("Verificar la información", NotificationType.error);
                return Redirect("~/Foto");
            }
        }

        public void CargarDDL()
        {
            //aqui agregamos los ddl : crear primero el modelo CamionesDDL
            List<LugarDDL> listalug = new List<LugarDDL>();
            listalug.Insert(0, new LugarDDL { id_lugar = 0, nombre_lugar = "Seleccione un lugar" });

            using (TurismoMexicoEntities1 context = new TurismoMexicoEntities1())
            {

                foreach (var c in context.lugares)
                {
                    //por cada camion se crea un auxiliar y se añade a la lista
                    LugarDDL aux = new LugarDDL();
                    aux.id_lugar = c.id_lugar;
                    aux.nombre_lugar = c.nombre;
                    listalug.Add(aux);
                }
            }
            ViewBag.ListaLugares = listalug;
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