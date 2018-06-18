namespace AplicatieSalariati.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cacat : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ConcediiModels");
            AddColumn("dbo.ConcediiModels", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ConcediiModels", "CNP", c => c.String());
            AddPrimaryKey("dbo.ConcediiModels", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ConcediiModels");
            AlterColumn("dbo.ConcediiModels", "CNP", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ConcediiModels", "Id");
            AddPrimaryKey("dbo.ConcediiModels", "CNP");
        }
    }
}
