namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correcao : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Veiculo", "TipoServico");
            DropColumn("dbo.Veiculo", "Categoria");
            DropColumn("dbo.Veiculo", "AcessoManobra");
            DropColumn("dbo.Solicitacao", "TipoEmissao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Solicitacao", "TipoEmissao", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "AcessoManobra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Veiculo", "Categoria", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "TipoServico", c => c.Int(nullable: false));
        }
    }
}
