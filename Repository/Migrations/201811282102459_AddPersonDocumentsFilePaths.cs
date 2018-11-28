namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonDocumentsFilePaths : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "CNHFilePath", c => c.String());
            AddColumn("dbo.Pessoa", "CTPSFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "CTPSFilePath");
            DropColumn("dbo.Pessoa", "CNHFilePath");
        }
    }
}
