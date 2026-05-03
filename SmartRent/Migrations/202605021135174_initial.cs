namespace SmartRent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Automobili",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Gorivo = c.String(nullable: false),
                        Karoserija = c.String(nullable: false),
                        Snaga = c.Int(nullable: false),
                        Zapremina = c.Int(nullable: false),
                        Boja = c.String(nullable: false),
                        Menjac = c.String(nullable: false),
                        ImgPath = c.String(),
                        Model_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Modeli", t => t.Model_Id, cascadeDelete: true)
                .Index(t => t.Model_Id);
            
            CreateTable(
                "dbo.Modeli",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Naziv = c.String(nullable: false, maxLength: 128),
                        Godina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marke", t => t.Naziv, cascadeDelete: true)
                .Index(t => t.Naziv);
            
            CreateTable(
                "dbo.Marke",
                c => new
                    {
                        Naziv = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Naziv);
            
            CreateTable(
                "dbo.Iznajmljivanja",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DatumIznajmljivanja = c.DateTime(nullable: false),
                        DatumVracanja = c.DateTime(nullable: false),
                        Automobil_Id = c.Int(nullable: false),
                        Korisnik_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Automobili", t => t.Automobil_Id, cascadeDelete: true)
                .ForeignKey("dbo.Korisnici", t => t.Korisnik_Id, cascadeDelete: true)
                .Index(t => t.Automobil_Id)
                .Index(t => t.Korisnik_Id);
            
            CreateTable(
                "dbo.Korisnici",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ime = c.String(nullable: false, maxLength: 20),
                        Prezime = c.String(nullable: false, maxLength: 20),
                        Lozinka = c.String(nullable: false),
                        KorisnickoIme = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.KorisnickoIme, unique: true, name: "KorisnickoIme");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Iznajmljivanja", "Korisnik_Id", "dbo.Korisnici");
            DropForeignKey("dbo.Iznajmljivanja", "Automobil_Id", "dbo.Automobili");
            DropForeignKey("dbo.Automobili", "Model_Id", "dbo.Modeli");
            DropForeignKey("dbo.Modeli", "Naziv", "dbo.Marke");
            DropIndex("dbo.Korisnici", "KorisnickoIme");
            DropIndex("dbo.Iznajmljivanja", new[] { "Korisnik_Id" });
            DropIndex("dbo.Iznajmljivanja", new[] { "Automobil_Id" });
            DropIndex("dbo.Modeli", new[] { "Naziv" });
            DropIndex("dbo.Automobili", new[] { "Model_Id" });
            DropTable("dbo.Korisnici");
            DropTable("dbo.Iznajmljivanja");
            DropTable("dbo.Marke");
            DropTable("dbo.Modeli");
            DropTable("dbo.Automobili");
        }
    }
}
