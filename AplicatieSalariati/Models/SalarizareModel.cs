using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.Models
{
    public class SalarizareModel//: IValidatableObject
    {
        [Key]
        [Display(Name = "CNP")]
        public string CNP { get; set; }

        [Display(Name = "Salar Brut")]
        [Required]
        public double Salar_Brut { get; set; }

        //[Display(Name = "Salar Realizat")]
        //[Required]
        //public double Salar_Realizat { get; set; }  

        [Required]
        [Display(Name = "Salar Net")]
        public double Salar_Net { get; set; }

        [Required]
        [Display(Name = "Impozit Venit")]
        public double IV { get; set; }

        [Required]
        [Display(Name = "CAS")]
        public double CAS { get; set; }

        [Required]
        public double CASS { get; set; }

        [Required]
        public double CAM { get; set; }

        [Required]
        [Display(Name = "Numar de bonuri de masa")]
        public double NrBonuri { get; set; }

        [Required]
        [Display(Name = "Valoarea unui bon de masa")]
        public double ValBonuri { get; set; }

    }
}