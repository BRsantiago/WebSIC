namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterTypeOfPropertiesInPessoaEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "NomeCompleto", c => c.String(nullable: false));
            AlterColumn("dbo.Pessoa", "Nome", c => c.String());
            AlterColumn("dbo.Pessoa", "DataNascimento", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pessoa", "Numero", c => c.Int());
            AlterColumn("dbo.Pessoa", "TelefoneEmergencia", c => c.String());
            AlterColumn("dbo.Pessoa", "DataValidadeCNH", c => c.DateTime());
            DropColumn("dbo.Pessoa", "Apelido");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pessoa", "Apelido", c => c.String());
            AlterColumn("dbo.Pessoa", "DataValidadeCNH", c => c.String());
            AlterColumn("dbo.Pessoa", "TelefoneEmergencia", c => c.String(nullable: false));
            AlterColumn("dbo.Pessoa", "Numero", c => c.String());
            AlterColumn("dbo.Pessoa", "DataNascimento", c => c.String());
            AlterColumn("dbo.Pessoa", "Nome", c => c.String(nullable: false));
            DropColumn("dbo.Pessoa", "NomeCompleto");
        }
    }
}
