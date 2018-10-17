namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLogotipoToEmpresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "ImageUrl", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "ImageUrl");
        }
    }
}
