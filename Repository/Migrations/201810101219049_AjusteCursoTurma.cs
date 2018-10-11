namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteCursoTurma : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", "dbo.TipoCrachas");
            DropIndex("dbo.TipoEmpresas", new[] { "TipoCracha_IdTipoCracha" });
            AddColumn("dbo.Turma", "Titulo", c => c.String());
            AddColumn("dbo.Turma", "Realizacao", c => c.DateTime(nullable: false));
            AddColumn("dbo.Turma", "DataValidade", c => c.DateTime(nullable: false));
            AddColumn("dbo.Curso", "Titulo", c => c.String(nullable: false));
            AddColumn("dbo.Curso", "CargaHoraria", c => c.Int(nullable: false));
            AddColumn("dbo.Curso", "Objetivos", c => c.String());
            AddColumn("dbo.Curso", "Validade", c => c.Int(nullable: false));
            AddColumn("dbo.Curso", "Prazo", c => c.Int(nullable: false));
            AddColumn("dbo.Curso", "Obrigatorio", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Contrato", "Numero", c => c.String(nullable: false));
            AlterColumn("dbo.PortaoAcesso", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.TipoSolicitacao", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.TipoEmpresas", "Descricao", c => c.String(nullable: false));
            AlterColumn("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", c => c.Int(nullable: false));
            AlterColumn("dbo.TipoCrachas", "Descricao", c => c.String(nullable: false));
            CreateIndex("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha");
            AddForeignKey("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", "dbo.TipoCrachas", "IdTipoCracha", cascadeDelete: true);
            DropColumn("dbo.Turma", "Inicio");
            DropColumn("dbo.Turma", "Fim");
            DropColumn("dbo.Curso", "Duracao");
            DropColumn("dbo.Curso", "Observacao");
            DropColumn("dbo.Curso", "DataVencimento");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Curso", "DataVencimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Curso", "Observacao", c => c.String());
            AddColumn("dbo.Curso", "Duracao", c => c.Int(nullable: false));
            AddColumn("dbo.Turma", "Fim", c => c.DateTime(nullable: false));
            AddColumn("dbo.Turma", "Inicio", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", "dbo.TipoCrachas");
            DropIndex("dbo.TipoEmpresas", new[] { "TipoCracha_IdTipoCracha" });
            AlterColumn("dbo.TipoCrachas", "Descricao", c => c.String());
            AlterColumn("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", c => c.Int());
            AlterColumn("dbo.TipoEmpresas", "Descricao", c => c.String());
            AlterColumn("dbo.TipoSolicitacao", "Descricao", c => c.String());
            AlterColumn("dbo.PortaoAcesso", "Descricao", c => c.String());
            AlterColumn("dbo.Contrato", "Numero", c => c.String());
            DropColumn("dbo.Curso", "Obrigatorio");
            DropColumn("dbo.Curso", "Prazo");
            DropColumn("dbo.Curso", "Validade");
            DropColumn("dbo.Curso", "Objetivos");
            DropColumn("dbo.Curso", "CargaHoraria");
            DropColumn("dbo.Curso", "Titulo");
            DropColumn("dbo.Turma", "DataValidade");
            DropColumn("dbo.Turma", "Realizacao");
            DropColumn("dbo.Turma", "Titulo");
            CreateIndex("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha");
            AddForeignKey("dbo.TipoEmpresas", "TipoCracha_IdTipoCracha", "dbo.TipoCrachas", "IdTipoCracha");
        }
    }
}
