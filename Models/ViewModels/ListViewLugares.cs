using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class ListViewLugares
    {
        public int id_lugar { get; set; }
        public int categoria_id { get; set; }
        public string nombre_categoria { get; set; }
        public string nombre_lugar { get; set; }
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
    }
}