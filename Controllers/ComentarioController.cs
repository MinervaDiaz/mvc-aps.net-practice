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
    public class ComentarioController : Controller
    {
        // GET: Comentario
        public ActionResult Index()
        {
            List<ComentariosLista> lista = new List<ComentariosLista>();
            using (TurismoMexicoEntities1 context = new TurismoMexicoEntities1())
            {
                lista = (from c in context.comentarios
                         join lug in context.lugares on c.lugar_id equals lug.id_lugar
                         select new ComentariosLista
                         {
                             id_comentario=c.id_comentario,
                             nombre_lugar=lug.nombre,
                             nombre_usuario=c.nombre_usuario,
                             comentario=c.comentario,
                             fecha=c.fecha,
                         }
                ).ToList();
            }
            return View(lista);
        }
        public ActionResult Nuevo_Comentario()
        {
            CargarDDL();
            return View();
        }

        [HttpPost]
        public ActionResult Nuevo_Comentario(NuevoComentario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                    {
                        var comentario = new comentarios();
                        comentario.id_comentario = model.id_comentario;
                        comentario.lugar_id = model.lugar_id;
                        comentario.nombre_usuario = model.nombre_usuario;
                        comentario.comentario = model.comentario;
                        comentario.fecha = model.fecha;
                        db.comentarios.Add(comentario);
                        db.SaveChanges();
                        CargarDDL();
                        Alert("Registro guardado con éxito", NotificationType.success);
                    }
                    return Redirect("~/Comentario");
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
        public ActionResult Eliminar_Comentario(int id)
        {
            comentarios comentario = new comentarios();
            try
            {
                using (TurismoMexicoEntities1 db = new TurismoMexicoEntities1())
                {
                    comentario = db.comentarios.Where(x => x.id_comentario == id).FirstOrDefault();
                    db.comentarios.Remove(comentario);
                    db.SaveChanges();
                }
                Alert("Eliminación exitosa", NotificationType.success);
                return Redirect("~/Comentario");
            }
            catch (Exception ex)
            {
                Alert("Error: " + ex.Message, NotificationType.error);
                return Redirect("~/Comentario");
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