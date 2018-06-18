namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class net : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalarizareModels", "Salar_Net", c => c.Double(nullable: false));
            DropColumn("dbo.SalarizareModels", "Total_Brut");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalarizareModels", "Total_Brut", c => c.Double(nullable: false));
            DropColumn("dbo.SalarizareModels", "Salar_Net");
        }
    }
}
