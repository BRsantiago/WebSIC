namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CursoUmPraMuitasAreas : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PessoaEmpresas", newName: "EmpresaPessoas");
            RenameTable(name: "dbo.TurmaPessoas", newName: "PessoaTurmas");
            DropForeignKey("dbo.Curso", "Area_IdArea", "dbo.Area");
            DropIndex("dbo.Curso", new[] { "Area_IdArea" });
            DropPrimaryKey("dbo.EmpresaPessoas");
            DropPrimaryKey("dbo.PessoaTurmas");
            CreateTable(
                "dbo.CursoAreas",
                c => new
                    {
                        Curso_IdCurso = c.Int(nullable: false),
                        Area_IdArea = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Curso_IdCurso, t.Area_IdArea })
                .ForeignKey("dbo.Curso", t => t.Curso_IdCurso, cascadeDelete: true)
                .ForeignKey("dbo.Area", t => t.Area_IdArea, cascadeDelete: true)
                .Index(t => t.Curso_IdCurso)
                .Index(t => t.Area_IdArea);
            
            AlterColumn("dbo.Pessoa", "FlgCVE", c => c.Boolean(nullable: false));
            AddPrimaryKey("dbo.EmpresaPessoas", new[] { "Empresa_IdEmpresa", "Pessoa_IdPessoa" });
            AddPrimaryKey("dbo.PessoaTurmas", new[] { "Pessoa_IdPessoa", "Turma_IdTurma" });
            DropColumn("dbo.Curso", "Area_IdArea");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Curso", "Area_IdArea", c => c.Int());
            DropForeignKey("dbo.CursoAreas", "Area_IdArea", "dbo.Area");
            DropForeignKey("dbo.CursoAreas", "Curso_IdCurso", "dbo.Curso");
            DropIndex("dbo.CursoAreas", new[] { "Area_IdArea" });
            DropIndex("dbo.CursoAreas", new[] { "Curso_IdCurso" });
            DropPrimaryKey("dbo.PessoaTurmas");
            DropPrimaryKey("dbo.EmpresaPessoas");
            AlterColumn("dbo.Pessoa", "FlgCVE", c => c.String());
            DropTable("dbo.CursoAreas");
            AddPrimaryKey("dbo.PessoaTurmas", new[] { "Turma_IdTurma", "Pessoa_IdPessoa" });
            AddPrimaryKey("dbo.EmpresaPessoas", new[] { "Pessoa_IdPessoa", "Empresa_IdEmpresa" });
            CreateIndex("dbo.Curso", "Area_IdArea");
            AddForeignKey("dbo.Curso", "Area_IdArea", "dbo.Area", "IdArea");
            RenameTable(name: "dbo.PessoaTurmas", newName: "TurmaPessoas");
            RenameTable(name: "dbo.EmpresaPessoas", newName: "PessoaEmpresas");
        }
    }
}
