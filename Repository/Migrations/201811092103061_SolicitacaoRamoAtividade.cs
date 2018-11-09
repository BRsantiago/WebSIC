namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitacaoRamoAtividade : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solicitacao", "RamoAtividade", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicitacao", "RamoAtividade");
        }
    }
}
