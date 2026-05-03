using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SmartRent.Models
{
    public class DBContext : DbContext
    {
        public DBContext():base("SmartRent")
        {

        }

        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Marka> Marke { get; set; }
        public DbSet<Model> Modeli { get; set; }
        public DbSet<Automobil> Automobili { get; set; }
        public DbSet<Iznajmljivanje> Iznajmljivanja { get; set; }
    }
}