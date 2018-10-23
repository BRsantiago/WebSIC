namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddInfoVeiculos : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso");
            DropForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropIndex("dbo.CursoSemTurma", new[] { "Curso_IdCurso" });
            DropIndex("dbo.CursoSemTurma", new[] { "Pessoa_IdPessoa" });
            AddColumn("dbo.Veiculo", "TipoServico", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "Categoria", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "AcessoManobra", c => c.Boolean(nullable: false));
            DropTable("dbo.CursoSemTurma");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CursoSemTurma",
                c => new
                    {
                        IdCursoSemTurma = c.Int(nullable: false, identity: true),
                        DataValidade = c.DateTime(nullable: false),
                        Criacao = c.DateTime(nullable: false),
                        Criador = c.String(),
                        Atualizacao = c.DateTime(nullable: false),
                        Atualizador = c.String(),
                        Ativo = c.Boolean(nullable: false),
                        Curso_IdCurso = c.Int(nullable: false),
                        Pessoa_IdPessoa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdCursoSemTurma);
            
            DropColumn("dbo.Veiculo", "AcessoManobra");
            DropColumn("dbo.Veiculo", "Categoria");
            DropColumn("dbo.Veiculo", "TipoServico");
            CreateIndex("dbo.CursoSemTurma", "Pessoa_IdPessoa");
            CreateIndex("dbo.CursoSemTurma", "Curso_IdCurso");
            AddForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa", "IdPessoa", cascadeDelete: true);
            AddForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso", "IdCurso", cascadeDelete: true);
        }
    }
}
