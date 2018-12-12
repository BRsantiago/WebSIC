namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PessoaColete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "NumeroColete", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "NumeroColete");
        }
    }
}
