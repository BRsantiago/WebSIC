namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterInsertExpiredDateInPersonPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "DataValidadeFoto", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "DataValidadeFoto");
        }
    }
}
