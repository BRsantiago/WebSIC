namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CredencialAddContratoPortaoAcesso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credencial", "Contrato_IdContrato", c => c.Int());
            AddColumn("dbo.Credencial", "PortaoAcesso_IdPortaoAcesso", c => c.Int());
            CreateIndex("dbo.Credencial", "Contrato_IdContrato");
            CreateIndex("dbo.Credencial", "PortaoAcesso_IdPortaoAcesso");
            AddForeignKey("dbo.Credencial", "Contrato_IdContrato", "dbo.Contrato", "IdContrato");
            AddForeignKey("dbo.Credencial", "PortaoAcesso_IdPortaoAcesso", "dbo.PortaoAcesso", "IdPortaoAcesso");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Credencial", "PortaoAcesso_IdPortaoAcesso", "dbo.PortaoAcesso");
            DropForeignKey("dbo.Credencial", "Contrato_IdContrato", "dbo.Contrato");
            DropIndex("dbo.Credencial", new[] { "PortaoAcesso_IdPortaoAcesso" });
            DropIndex("dbo.Credencial", new[] { "Contrato_IdContrato" });
            DropColumn("dbo.Credencial", "PortaoAcesso_IdPortaoAcesso");
            DropColumn("dbo.Credencial", "Contrato_IdContrato");
        }
    }
}
