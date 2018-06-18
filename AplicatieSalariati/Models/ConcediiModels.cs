using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicatieSalariati.Models
{
    public class ConcediiModel
    {
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "CNP")]
        public string CNP { get; set; }

        [Required]
        public DateTime dataStart { get; set; }

        [Required]
        public DateTime DataFinal { get; set; }

        public bool Confirmat { get; set; }

    }
}
