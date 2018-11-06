namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTipoCrachaToInputFileName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCrachas", "Arquivo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoCrachas", "Arquivo");
        }
    }
}
