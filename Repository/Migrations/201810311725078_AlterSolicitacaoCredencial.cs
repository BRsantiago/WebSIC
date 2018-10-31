namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterSolicitacaoCredencial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credencial", "NomeImpressaoFrenteCracha", c => c.String());
            AddColumn("dbo.Credencial", "DescricaoFuncaoFrenteCracha", c => c.String());
            AddColumn("dbo.Solicitacao", "CaminhoArquivoDigitalizado", c => c.String());
            AddColumn("dbo.Solicitacao", "Cargo_IdCargo", c => c.Int());
            CreateIndex("dbo.Solicitacao", "Cargo_IdCargo");
            AddForeignKey("dbo.Solicitacao", "Cargo_IdCargo", "dbo.Cargo", "IdCargo");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Solicitacao", "Cargo_IdCargo", "dbo.Cargo");
            DropIndex("dbo.Solicitacao", new[] { "Cargo_IdCargo" });
            DropColumn("dbo.Solicitacao", "Cargo_IdCargo");
            DropColumn("dbo.Solicitacao", "CaminhoArquivoDigitalizado");
            DropColumn("dbo.Credencial", "DescricaoFuncaoFrenteCracha");
            DropColumn("dbo.Credencial", "NomeImpressaoFrenteCracha");
        }
    }
}
