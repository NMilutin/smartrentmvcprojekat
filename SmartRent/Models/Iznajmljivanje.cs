using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRent.Models
{
    [Table("Iznajmljivanja")]
    public class Iznajmljivanje
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Automobil je obavezan")]
        public Automobil Automobil { get; set; }
        [Required(ErrorMessage = "Korisnik je obavezan")]
        public Korisnik Korisnik { get; set; }
        [Required(ErrorMessage = "Datum iznajmljivanja je obavezan")]
        public DateTime DatumIznajmljivanja { get; set; }
        public DateTime DatumVracanja { get; set; }
    }
}