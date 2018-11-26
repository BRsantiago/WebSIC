namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCredentialAndSolicitacaoEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credencial", "FlgSegundaVia", c => c.Boolean(nullable: false));
            AddColumn("dbo.TipoSolicitacao", "FlgGeraSegundaVia", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoSolicitacao", "FlgGeraSegundaVia");
            DropColumn("dbo.Credencial", "FlgSegundaVia");
        }
    }
}
