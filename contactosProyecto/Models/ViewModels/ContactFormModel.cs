using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace contactosProyecto.Models.ViewModels
{
    public class ContactFormModel
    {
        [Key]
        public int Id { get;set; }

        [Required(ErrorMessage ="Nombre es requerido")]
        [StringLength(20)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Numero es requerido")]
        [StringLength(50)]
        [Display(Name = "Numero")]
        public string Number { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Detalle")]
        public string Detail { get; set; }

        [Required]
        [Display(Name = "Favorito")]
        public bool Favorite { get; set; }

        [Required]
        [Display(Name = "Emergencia")]
        public bool Emergency { get; set; }


    }
}