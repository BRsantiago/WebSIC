namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCursoSemTurma : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PessoaTurmas", newName: "TurmaPessoas");
            DropPrimaryKey("dbo.TurmaPessoas");
            AddPrimaryKey("dbo.TurmaPessoas", new[] { "Turma_IdTurma", "Pessoa_IdPessoa" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TurmaPessoas");
            AddPrimaryKey("dbo.TurmaPessoas", new[] { "Pessoa_IdPessoa", "Turma_IdTurma" });
            RenameTable(name: "dbo.TurmaPessoas", newName: "PessoaTurmas");
        }
    }
}
