namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAcessoAreaManobraManipulaBagagem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Credencial", "ManipulaBagagem", c => c.Boolean(nullable: false));
            AddColumn("dbo.Credencial", "AcessoAreaManobra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Solicitacao", "DataVencimento", c => c.DateTime());
            AddColumn("dbo.Solicitacao", "ManipulaBagagem", c => c.Boolean(nullable: false));
            AddColumn("dbo.Solicitacao", "AcessoAreaManobra", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicitacao", "AcessoAreaManobra");
            DropColumn("dbo.Solicitacao", "ManipulaBagagem");
            DropColumn("dbo.Solicitacao", "DataVencimento");
            DropColumn("dbo.Credencial", "AcessoAreaManobra");
            DropColumn("dbo.Credencial", "ManipulaBagagem");
        }
    }
}
