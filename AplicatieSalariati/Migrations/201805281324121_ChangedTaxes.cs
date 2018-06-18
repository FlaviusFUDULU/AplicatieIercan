namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTaxes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaxePrestabiliteModels", "CASS", c => c.Double(nullable: false));
            AddColumn("dbo.TaxePrestabiliteModels", "CAM", c => c.Double(nullable: false));
            DropColumn("dbo.TaxePrestabiliteModels", "Somaj");
            DropColumn("dbo.TaxePrestabiliteModels", "Sanatate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaxePrestabiliteModels", "Sanatate", c => c.Double(nullable: false));
            AddColumn("dbo.TaxePrestabiliteModels", "Somaj", c => c.Double(nullable: false));
            DropColumn("dbo.TaxePrestabiliteModels", "CAM");
            DropColumn("dbo.TaxePrestabiliteModels", "CASS");
        }
    }
}
