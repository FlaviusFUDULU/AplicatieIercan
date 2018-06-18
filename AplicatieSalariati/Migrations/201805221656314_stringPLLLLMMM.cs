namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringPLLLLMMM : DbMigration
    {
        public override void Up()
        {
            
            CreateTable(
                "dbo.SalariatModels",
                c => new
                    {
                        CNP = c.String(nullable: false, maxLength: 128),
                        Nume = c.String(nullable: false),
                        Prenume = c.String(nullable: false),
                        Functie = c.String(nullable: false),
                        Salar_Brut = c.Double(nullable: false),
                        Salar_Realizat = c.Double(nullable: false),
                        Vechime = c.Double(nullable: false),
                        Spor = c.Double(nullable: false),
                        Premii_Brute = c.Int(nullable: false),
                        Compensatie = c.Double(nullable: false),
                        Total_Brut = c.Double(nullable: false),
                        Brut_Impozabil = c.Double(nullable: false),
                        Impozit = c.Double(nullable: false),
                        CAS = c.Double(nullable: false),
                        Somaj = c.Double(nullable: false),
                        Sanatate = c.Double(nullable: false),
                        Avans = c.Double(nullable: false),
                        Retineri = c.Double(nullable: false),
                        RestPlata = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CNP);
            
            
        }

        public override void Down()
        {

            DropTable("dbo.SalariatModels");
        }
    }
}
