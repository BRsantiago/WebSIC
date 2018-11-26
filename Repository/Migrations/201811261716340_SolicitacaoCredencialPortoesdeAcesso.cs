namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitacaoCredencialPortoesdeAcesso : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Solicitacao", name: "PortaoAcesso_IdPortaoAcesso", newName: "PortaoAcesso1_IdPortaoAcesso");
            RenameColumn(table: "dbo.Credencial", name: "PortaoAcesso_IdPortaoAcesso", newName: "PortaoAcesso1_IdPortaoAcesso");
            RenameIndex(table: "dbo.Credencial", name: "IX_PortaoAcesso_IdPortaoAcesso", newName: "IX_PortaoAcesso1_IdPortaoAcesso");
            RenameIndex(table: "dbo.Solicitacao", name: "IX_PortaoAcesso_IdPortaoAcesso", newName: "IX_PortaoAcesso1_IdPortaoAcesso");
            AddColumn("dbo.Credencial", "PortaoAcesso2_IdPortaoAcesso", c => c.Int());
            AddColumn("dbo.Credencial", "PortaoAcesso3_IdPortaoAcesso", c => c.Int());
            AddColumn("dbo.Solicitacao", "PortaoAcesso2_IdPortaoAcesso", c => c.Int());
            AddColumn("dbo.Solicitacao", "PortaoAcesso3_IdPortaoAcesso", c => c.Int());
            CreateIndex("dbo.Credencial", "PortaoAcesso2_IdPortaoAcesso");
            CreateIndex("dbo.Credencial", "PortaoAcesso3_IdPortaoAcesso");
            CreateIndex("dbo.Solicitacao", "PortaoAcesso2_IdPortaoAcesso");
            CreateIndex("dbo.Solicitacao", "PortaoAcesso3_IdPortaoAcesso");
            AddForeignKey("dbo.Solicitacao", "PortaoAcesso2_IdPortaoAcesso", "dbo.PortaoAcesso", "IdPortaoAcesso");
            AddForeignKey("dbo.Solicitacao", "PortaoAcesso3_IdPortaoAcesso", "dbo.PortaoAcesso", "IdPortaoAcesso");
            AddForeignKey("dbo.Credencial", "PortaoAcesso2_IdPortaoAcesso", "dbo.PortaoAcesso", "IdPortaoAcesso");
            AddForeignKey("dbo.Credencial", "PortaoAcesso3_IdPortaoAcesso", "dbo.PortaoAcesso", "IdPortaoAcesso");
            DropColumn("dbo.Pessoa", "FlgResidenciaForaDoPaisNosUltimos10Anos");
            DropColumn("dbo.Pessoa", "ObservacaoResidenciaForaDoPaisNosUltimos10Anos");
            DropColumn("dbo.Credencial", "FlgSegundaVia");
            DropColumn("dbo.TipoSolicitacao", "FlgGeraSegundaVia");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TipoSolicitacao", "FlgGeraSegundaVia", c => c.Boolean(nullable: false));
            AddColumn("dbo.Credencial", "FlgSegundaVia", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pessoa", "ObservacaoResidenciaForaDoPaisNosUltimos10Anos", c => c.String());
            AddColumn("dbo.Pessoa", "FlgResidenciaForaDoPaisNosUltimos10Anos", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Credencial", "PortaoAcesso3_IdPortaoAcesso", "dbo.PortaoAcesso");
            DropForeignKey("dbo.Credencial", "PortaoAcesso2_IdPortaoAcesso", "dbo.PortaoAcesso");
            DropForeignKey("dbo.Solicitacao", "PortaoAcesso3_IdPortaoAcesso", "dbo.PortaoAcesso");
            DropForeignKey("dbo.Solicitacao", "PortaoAcesso2_IdPortaoAcesso", "dbo.PortaoAcesso");
            DropIndex("dbo.Solicitacao", new[] { "PortaoAcesso3_IdPortaoAcesso" });
            DropIndex("dbo.Solicitacao", new[] { "PortaoAcesso2_IdPortaoAcesso" });
            DropIndex("dbo.Credencial", new[] { "PortaoAcesso3_IdPortaoAcesso" });
            DropIndex("dbo.Credencial", new[] { "PortaoAcesso2_IdPortaoAcesso" });
            DropColumn("dbo.Solicitacao", "PortaoAcesso3_IdPortaoAcesso");
            DropColumn("dbo.Solicitacao", "PortaoAcesso2_IdPortaoAcesso");
            DropColumn("dbo.Credencial", "PortaoAcesso3_IdPortaoAcesso");
            DropColumn("dbo.Credencial", "PortaoAcesso2_IdPortaoAcesso");
            RenameIndex(table: "dbo.Solicitacao", name: "IX_PortaoAcesso1_IdPortaoAcesso", newName: "IX_PortaoAcesso_IdPortaoAcesso");
            RenameIndex(table: "dbo.Credencial", name: "IX_PortaoAcesso1_IdPortaoAcesso", newName: "IX_PortaoAcesso_IdPortaoAcesso");
            RenameColumn(table: "dbo.Credencial", name: "PortaoAcesso1_IdPortaoAcesso", newName: "PortaoAcesso_IdPortaoAcesso");
            RenameColumn(table: "dbo.Solicitacao", name: "PortaoAcesso1_IdPortaoAcesso", newName: "PortaoAcesso_IdPortaoAcesso");
        }
    }
}
