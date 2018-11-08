namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterPessoa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "DataValidadeFoto", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "DataValidadeFoto", c => c.DateTime(nullable: false));
        }
    }
}
