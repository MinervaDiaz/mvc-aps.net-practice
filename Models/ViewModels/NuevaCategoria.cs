using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class NuevaCategoria
    {
        public int id_categoria { get; set; }

        [Required]//datavalidator   EL VALOR DEBAJO DEL REQUIRED NO DEBE IR NULO
        [Display(Name = "nombre")]
        public string nombre { get; set; }
        
        [Display(Name = "descripcion")]
        public string descripcion { get; set; }
    }
}