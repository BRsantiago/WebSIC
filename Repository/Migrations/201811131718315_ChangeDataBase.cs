namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDataBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoSolicitacao", "FlgGeraNovaCredencial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoSolicitacao", "FlgGeraNovaCredencial");
        }
    }
}
