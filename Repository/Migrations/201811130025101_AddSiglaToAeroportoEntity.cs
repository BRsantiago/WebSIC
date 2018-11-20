namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSiglaToAeroportoEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Aeroporto", "Sigla", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Aeroporto", "Sigla");
        }
    }
}
