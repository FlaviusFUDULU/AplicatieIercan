namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSalarizare : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalarizareModels", "IV", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "CASS", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "CAM", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "NrBonuri", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "ValBonuri", c => c.Double(nullable: false));
            DropColumn("dbo.SalarizareModels", "Vechime");
            DropColumn("dbo.SalarizareModels", "Spor");
            DropColumn("dbo.SalarizareModels", "Premii_Brute");
            DropColumn("dbo.SalarizareModels", "Compensatie");
            DropColumn("dbo.SalarizareModels", "Brut_Impozabil");
            DropColumn("dbo.SalarizareModels", "Impozit");
            DropColumn("dbo.SalarizareModels", "CAS");
            DropColumn("dbo.SalarizareModels", "Somaj");
            DropColumn("dbo.SalarizareModels", "Sanatate");
            DropColumn("dbo.SalarizareModels", "Avans");
            DropColumn("dbo.SalarizareModels", "Retineri");
            DropColumn("dbo.SalarizareModels", "RestPlata");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalarizareModels", "RestPlata", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Retineri", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Avans", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Sanatate", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Somaj", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "CAS", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Impozit", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Brut_Impozabil", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Compensatie", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Premii_Brute", c => c.Int(nullable: false));
            AddColumn("dbo.SalarizareModels", "Spor", c => c.Double(nullable: false));
            AddColumn("dbo.SalarizareModels", "Vechime", c => c.Double(nullable: false));
            DropColumn("dbo.SalarizareModels", "ValBonuri");
            DropColumn("dbo.SalarizareModels", "NrBonuri");
            DropColumn("dbo.SalarizareModels", "CAM");
            DropColumn("dbo.SalarizareModels", "CASS");
            DropColumn("dbo.SalarizareModels", "IV");
        }
    }
}
