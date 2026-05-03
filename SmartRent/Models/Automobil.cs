using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRent.Models
{
    [Table("Automobili")]
    public class Automobil
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vrsta goriva je obavezna")]
        public string Gorivo { get; set; }
        [Required(ErrorMessage = "Tip karoserije je obavezan")]
        public string Karoserija { get; set; }
        [Required(ErrorMessage = "Snaga motora je obavezna")]
        public int Snaga { get; set; } // Snaga motora je u KS
        [Required(ErrorMessage = "Zapremina motora je obavezna")]
        public int Zapremina { get; set; } // Zapremina je u bazi u cm3 prikazuje se u litrima
        [Required(ErrorMessage = "Boja je obavezna")]
        public string Boja { get; set; }
        [Required(ErrorMessage = "Tip menjaca je obavezan")]
        public string Menjac { get; set; }
        [Required(ErrorMessage = "Cena je obavezna")]
        public double CenaZaDan { get; set; } // Cena je izrazena u evrima
        public string ImgPath { get; set; }
        [Required(ErrorMessage = "Model je obavezan")]
        public Model Model { get; set; }

        public override string ToString()
        {
            return $"{Model.Marka.Naziv} {Model.NazivModela} {Model.Godina}";
        }

        public string ToLongString()
        {
            return $"{Model.Marka.Naziv};{Model.NazivModela};{Model.Godina};{Gorivo};{Karoserija};{Snaga}KS;{((double)Zapremina / 1000).ToString("N1")};{Menjac}";
        }
    }
}