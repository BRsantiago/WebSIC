namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPessoaAddGenero : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "Genero", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "Genero", c => c.String());
        }
    }
}
