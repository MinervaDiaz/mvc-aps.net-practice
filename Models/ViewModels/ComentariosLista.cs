using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class ComentariosLista
    {
        public int id_comentario { get; set; }
        public int lugar_id { get; set; }
        public string nombre_lugar { get; set; }
        public string nombre_usuario { get; set; }
        public string comentario { get; set; }
        public Nullable<System.DateTime> fecha { get; set; }
    }
}