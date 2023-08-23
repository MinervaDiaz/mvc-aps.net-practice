using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class NuevaFoto
    {
        public int id_foto { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "lugar_id")]
        public int lugar_id { get; set; }
        [Required]
        [Display(Name = "url_foto")]
        public string url_foto { get; set; }
        public string descripcion { get; set; }
    }
}