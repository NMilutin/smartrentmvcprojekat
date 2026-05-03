namespace SmartRent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autocenaupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Automobili", "CenaZaDan", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Automobili", "CenaZaDan");
        }
    }
}
