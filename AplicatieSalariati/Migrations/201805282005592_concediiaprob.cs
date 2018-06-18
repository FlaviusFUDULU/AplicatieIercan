namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class concediiaprob : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConcediileModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CNP = c.String(),
                        Nume = c.String(nullable: false),
                        Prenume = c.String(nullable: false),
                        dataStart = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                        Confirmat = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ConcediileModels");
        }
    }
}
