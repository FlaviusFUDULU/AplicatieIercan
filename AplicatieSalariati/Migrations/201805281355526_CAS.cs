namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CAS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalarizareModels", "CAS", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SalarizareModels", "CAS");
        }
    }
}
