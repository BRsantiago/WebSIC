namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPersonDocumentsAttachments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "RGFilePath", c => c.String());
            AddColumn("dbo.Pessoa", "CRFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "CRFilePath");
            DropColumn("dbo.Pessoa", "RGFilePath");
        }
    }
}
