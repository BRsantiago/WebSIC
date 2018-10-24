namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FotoPessoa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "ImageUrl");
        }
    }
}
