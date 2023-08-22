using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class NuevoLugar
    {
        public int id_lugar { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "nombre")]
        public string nombre { get; set; }
        
        public string descripcion { get; set; }
        public string ubicacion { get; set; }
        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "categoria_id")]
        public int categoria_id { get; set; }

    }
}