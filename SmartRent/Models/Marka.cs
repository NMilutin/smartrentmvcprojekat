using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartRent.Models
{
    [Table("Marke")]
    public class Marka
    {
        [Key]
        public string Naziv { get; set; }
    }
}