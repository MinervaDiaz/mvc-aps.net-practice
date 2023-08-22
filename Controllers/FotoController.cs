using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurismoMexicoMVC.Models.ViewModels;
using TurismoMexicoMVC.Models;

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
    }
}