namespace SmartRent.Migrations
{
    using SmartRent.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SmartRent.Models.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SmartRent.Models.DBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            string[] marke =
            {
                "BMW",
                "Ford",
                "Mazda",
                "Renault",
                "Peugeot",
                "Volkswagen",
                "Toyota",
                "Opel",
                "Audi"
            };
            Marka[] markeObj = marke.Select(m => new Marka() { Naziv = m }).ToArray();
            foreach (var item in markeObj)
            {
                context.Marke.AddOrUpdate(item);
            }
            context.SaveChanges();

            Model[] modeli =
            {
                new Model()
                {
                    Id = 1,
                    Marka = context.Marke.Find("Peugeot"),
                    NazivModela = "308",
                    Godina = 2015
                },
                new Model()
                {
                    Id = 2,
                    Marka = context.Marke.Find("Audi"),
                    NazivModela = "A6",
                    Godina = 2020
                },
                new Model()
                {
                    Id = 3,
                    Marka = context.Marke.Find("Toyota"),
                    NazivModela = "Yaris",
                    Godina = 2023,
                },
                new Model()
                {
                    Id = 4,
                    Marka = context.Marke.Find("BMW"),
                    NazivModela = "320",
                    Godina = 2018
                }
            };
            foreach (var item in modeli)
            {
                context.Modeli.AddOrUpdate(item);
            }
            context.SaveChanges();

            Automobil[] automobili =
            {
                new Automobil()
                {
                    Id = 1,
                    Model = context.Modeli.Find(1),
                    Gorivo = "Dizel",
                    Karoserija = "Karavan",
                    Snaga = 147,
                    Zapremina = 1600,
                    Boja = "Siva",
                    Menjac = "Manuelni",
                    CenaZaDan = 30,
                    ImgPath = "/Content/Upload/peugeot-308.jpg"
                },
                new Automobil()
                {
                    Id = 2,
                    Model = context.Modeli.Find(2),
                    Gorivo = "Benzin",
                    Karoserija = "Limuzina",
                    Snaga = 250,
                    Zapremina = 2000,
                    Boja = "Crna",
                    Menjac = "Automatski",
                    CenaZaDan = 50,
                },
            };
            foreach (var item in automobili)
            {
                context.Automobili.AddOrUpdate(item);
            }
            context.SaveChanges();
        }
    }
}
