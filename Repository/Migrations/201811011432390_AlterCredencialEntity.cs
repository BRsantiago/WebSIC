namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCredencialEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "CategoriaUm", c => c.Int(nullable: false));
            AddColumn("dbo.Pessoa", "CategoriaDois", c => c.Int(nullable: false));
            AddColumn("dbo.Credencial", "CategoriaMotorista1", c => c.String());
            AddColumn("dbo.Credencial", "CategoriaMotorista2", c => c.String());
            AlterColumn("dbo.Credencial", "DataExpedicao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Credencial", "DataDesativacao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Credencial", "DataVencimento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Pessoa", "CategoriaCNH");
            DropColumn("dbo.Credencial", "Matricula");
            DropColumn("dbo.Credencial", "FlgMotorista");
            DropColumn("dbo.Solicitacao", "FlgMotorista");
            DropColumn("dbo.Solicitacao", "FlgTemporario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solicitacao", "FlgTemporario", c => c.Boolean(nullable: false));
            AddColumn("dbo.Solicitacao", "FlgMotorista", c => c.Boolean(nullable: false));
            AddColumn("dbo.Credencial", "FlgMotorista", c => c.Boolean(nullable: false));
            AddColumn("dbo.Credencial", "Matricula", c => c.String(nullable: false));
            AddColumn("dbo.Pessoa", "CategoriaCNH", c => c.String());
            AlterColumn("dbo.Credencial", "DataVencimento", c => c.String(nullable: false));
            AlterColumn("dbo.Credencial", "DataDesativacao", c => c.String());
            AlterColumn("dbo.Credencial", "DataExpedicao", c => c.String(nullable: false));
            DropColumn("dbo.Credencial", "CategoriaMotorista2");
            DropColumn("dbo.Credencial", "CategoriaMotorista1");
            DropColumn("dbo.Pessoa", "CategoriaDois");
            DropColumn("dbo.Pessoa", "CategoriaUm");
        }
    }
}
