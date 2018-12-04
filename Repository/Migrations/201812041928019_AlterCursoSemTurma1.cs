namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCursoSemTurma1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso");
            DropForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropIndex("dbo.CursoSemTurma", new[] { "Curso_IdCurso" });
            DropIndex("dbo.CursoSemTurma", new[] { "Pessoa_IdPessoa" });
            AlterColumn("dbo.CursoSemTurma", "Curso_IdCurso", c => c.Int());
            AlterColumn("dbo.CursoSemTurma", "Pessoa_IdPessoa", c => c.Int());
            CreateIndex("dbo.CursoSemTurma", "Curso_IdCurso");
            CreateIndex("dbo.CursoSemTurma", "Pessoa_IdPessoa");
            AddForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso", "IdCurso");
            AddForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa", "IdPessoa");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa");
            DropForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso");
            DropIndex("dbo.CursoSemTurma", new[] { "Pessoa_IdPessoa" });
            DropIndex("dbo.CursoSemTurma", new[] { "Curso_IdCurso" });
            AlterColumn("dbo.CursoSemTurma", "Pessoa_IdPessoa", c => c.Int(nullable: false));
            AlterColumn("dbo.CursoSemTurma", "Curso_IdCurso", c => c.Int(nullable: false));
            CreateIndex("dbo.CursoSemTurma", "Pessoa_IdPessoa");
            CreateIndex("dbo.CursoSemTurma", "Curso_IdCurso");
            AddForeignKey("dbo.CursoSemTurma", "Pessoa_IdPessoa", "dbo.Pessoa", "IdPessoa", cascadeDelete: true);
            AddForeignKey("dbo.CursoSemTurma", "Curso_IdCurso", "dbo.Curso", "IdCurso", cascadeDelete: true);
        }
    }
}
