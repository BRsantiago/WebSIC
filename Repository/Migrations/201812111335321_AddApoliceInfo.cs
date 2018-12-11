namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApoliceInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apolice", "DataInicioVigencia", c => c.DateTime(nullable: false));
            AddColumn("dbo.Apolice", "Seguro", c => c.String());
            AddColumn("dbo.Apolice", "Seguradora", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Apolice", "Seguradora");
            DropColumn("dbo.Apolice", "Seguro");
            DropColumn("dbo.Apolice", "DataInicioVigencia");
        }
    }
}
