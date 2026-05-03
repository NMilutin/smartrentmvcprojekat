namespace SmartRent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelnazivfix : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Modeli", name: "Naziv", newName: "Marka_Naziv");
            RenameIndex(table: "dbo.Modeli", name: "IX_Naziv", newName: "IX_Marka_Naziv");
            AddColumn("dbo.Modeli", "NazivModela", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Modeli", "NazivModela");
            RenameIndex(table: "dbo.Modeli", name: "IX_Marka_Naziv", newName: "IX_Naziv");
            RenameColumn(table: "dbo.Modeli", name: "Marka_Naziv", newName: "Naziv");
        }
    }
}
