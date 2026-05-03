using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRent.Models
{
    [Table("Korisnici")]
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Unesite Vase ime.")]
        [MaxLength(20,ErrorMessage = "Ime moze biti dugacko najvise 20 karaktera,")]
        public string Ime { get; set; }
        [Required(AllowEmptyStrings = true)]
        [MaxLength(20, ErrorMessage = "Prezime moze biti dugacko najvise 20 karaktera,")]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "Lozinka je obavezna")]
        public string Lozinka { get; set; }
        [Required(ErrorMessage = "Korisnicko ime je obavezno")]
        [MaxLength(20,ErrorMessage = "Korisnicko ime je predugacko")]
        [Index(nameof(KorisnickoIme),IsUnique = true)]
        public string KorisnickoIme { get; set; }
    }
}