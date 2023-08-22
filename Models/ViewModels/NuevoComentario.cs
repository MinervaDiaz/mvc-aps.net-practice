using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TurismoMexicoMVC.Models.ViewModels
{
    public class NuevoComentario
    {
        public int id_comentario { get; set; }
        [Required]
        [Display(Name = "lugar_id")]
        public int lugar_id { get; set; }
        public string nombre_lugar { get; set; }
        [Required]
        [Display(Name = "nombre_usuario")]
        public string nombre_usuario { get; set; }
        [Required]
        [Display(Name = "comentario")]
        public string comentario { get; set; }
        [Required]
        [Display(Name = "fecha")]
        public Nullable<System.DateTime> fecha { get; set; }
    }
}