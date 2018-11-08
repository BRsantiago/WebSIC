namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterDataBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TipoSolicitacao", "FlgDesativaCredencial", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TipoSolicitacao", "FlgDesativaCredencial");
        }
    }
}
