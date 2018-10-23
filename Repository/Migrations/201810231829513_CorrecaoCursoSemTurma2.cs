namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoCursoSemTurma2 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.IdCursoSemTurma)
                .ForeignKey("dbo.Curso", t => t.Curso_IdCurso, cascadeDelete: true)
                .ForeignKey("dbo.Pessoa", t => t.Pessoa_IdPessoa, cascadeDelete: true)
                .Index(t => t.Curso_IdCurso)
                .Index(t => t.Pessoa_IdPessoa);
            
            DropColumn("dbo.Veiculo", "TipoServico");
            DropColumn("dbo.Veiculo", "Categoria");
            DropColumn("dbo.Veiculo", "AcessoManobra");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Veiculo", "AcessoManobra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Veiculo", "Categoria", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "TipoServico", c => c.Int(nullable: false));
            DropForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso");
            DropIndex("dbo.CursoSemTurma", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.CursoSemTurma", new[] { "Curso_IdCurso" });
            DropTable("dbo.CursoSemTurma");
        }
    }
}
