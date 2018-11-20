namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSolicitacaoToCreateLinkToRamoAtividade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RamoAtividades",
                c => new
                    {
                        IdRamoAtividade = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdRamoAtividade);
            
            CreateTable(
                "dbo.RamoAtividadeCursoes",
                c => new
                    {
                        RamoAtividade_IdRamoAtividade = c.Int(nullable: false),
                        Curso_IdCurso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RamoAtividade_IdRamoAtividade, t.Curso_IdCurso })
                .ForeignKey("dbo.RamoAtividades", t => t.RamoAtividade_IdRamoAtividade, cascadeDelete: true)
                .ForeignKey("dbo.Curso", t => t.Curso_IdCurso, cascadeDelete: true)
                .Index(t => t.RamoAtividade_IdRamoAtividade)
                .Index(t => t.Curso_IdCurso);
            
            AddColumn("dbo.Solicitacao", "RamoAtividade_IdRamoAtividade", c => c.Int());
            CreateIndex("dbo.Solicitacao", "RamoAtividade_IdRamoAtividade");
            AddForeignKey("dbo.Solicitacao", "RamoAtividade_IdRamoAtividade", "dbo.RamoAtividades", "IdRamoAtividade");
            DropColumn("dbo.Solicitacao", "RamoAtividade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solicitacao", "RamoAtividade", c => c.Int(nullable: false));
            DropForeignKey("dbo.Solicitacao", "RamoAtividade_IdRamoAtividade", "dbo.RamoAtividades");
            DropForeignKey("dbo.RamoAtividadeCursoes", "Curso_IdCurso", "dbo.Curso");
            DropForeignKey("dbo.RamoAtividadeCursoes", "RamoAtividade_IdRamoAtividade", "dbo.RamoAtividades");
            DropIndex("dbo.RamoAtividadeCursoes", new[] { "Curso_IdCurso" });
            DropIndex("dbo.RamoAtividadeCursoes", new[] { "RamoAtividade_IdRamoAtividade" });
            DropIndex("dbo.Solicitacao", new[] { "RamoAtividade_IdRamoAtividade" });
            DropColumn("dbo.Solicitacao", "RamoAtividade_IdRamoAtividade");
            DropTable("dbo.RamoAtividadeCursoes");
            DropTable("dbo.RamoAtividades");
        }
    }
}
