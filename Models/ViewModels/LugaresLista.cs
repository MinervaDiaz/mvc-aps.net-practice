using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class LugaresLista
    {
        internal string nombre_categoria;

        public int id_lugar { get; set; }
        public string nombre{ get; set; }
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
        public int categoria_id { get; set; }
        public string nombre_cat { get; set; }
    }
}