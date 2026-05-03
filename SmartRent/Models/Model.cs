using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRent.Models
{
    [Table("Modeli")]
    public class Model
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Naziv modela je obavezan")]
        public string NazivModela { get; set; }
        [Required(ErrorMessage = "Godina modela je obavezna")]
        [Range(1900,2100)]
        public int Godina { get; set; }
        [Required(ErrorMessage = "Marka je obavezna")]
        public Marka Marka { get; set; }
    }
} 