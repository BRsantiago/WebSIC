namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrecaoCursoSemTurma : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso");
            DropIndex("dbo.CursoSemTurma", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.CursoSemTurma", new[] { "Curso_IdCurso" });
            DropTable("dbo.CursoSemTurma");
        }
    }
}
