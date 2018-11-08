namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTipoCrachaParaArmazenarTipoCrachaTemporario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoCrachas", "FlgCrachaTemporario", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoCrachas", "FlgCrachaTemporario");
        }
    }
}
