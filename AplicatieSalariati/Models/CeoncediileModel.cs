using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AplicatieSalariati.Models
{
    public class ConcediileModel
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "CNP")]
        public string CNP { get; set; }

        [Required]
        public string Nume { get; set; }

        [Required]
        public string Prenume { get; set; }

        [Required]
        public DateTime dataStart { get; set; }

        [Required]
        public DateTime DataFinal { get; set; }

        public bool Confirmat { get; set; }
    }
}
