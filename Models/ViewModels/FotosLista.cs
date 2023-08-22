using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class FotosLista
    {
        public int id_foto { get; set; }
        public int lugar_id { get; set; }        
        public string nombre_lugar { get; set; }
        public string url_foto { get; set; }
        public string descripcion { get; set; }
    }
}