namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterEmpresaToImageisnotrequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empresa", "ImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empresa", "ImageUrl", c => c.String(nullable: false));
        }
    }
}
