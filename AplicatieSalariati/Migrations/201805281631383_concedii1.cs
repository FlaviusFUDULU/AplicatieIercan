namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class concedii1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConcediiModels",
                c => new
                    {
                        CNP = c.String(nullable: false, maxLength: 128),
                        dataStart = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                        Confirmat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CNP);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConcediiModels");
        }
    }
}
