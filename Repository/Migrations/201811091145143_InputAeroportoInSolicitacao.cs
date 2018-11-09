namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InputAeroportoInSolicitacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solicitacao", "Aeroporto_IdAeroporto", c => c.Int());
            CreateIndex("dbo.Solicitacao", "Aeroporto_IdAeroporto");
            AddForeignKey("dbo.Solicitacao", "Aeroporto_IdAeroporto", "dbo.Aeroporto", "IdAeroporto");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solicitacao", "Aeroporto_IdAeroporto", "dbo.Aeroporto");
            DropIndex("dbo.Solicitacao", new[] { "Aeroporto_IdAeroporto" });
            DropColumn("dbo.Solicitacao", "Aeroporto_IdAeroporto");
        }
    }
}
