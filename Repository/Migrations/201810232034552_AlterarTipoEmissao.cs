namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarTipoEmissao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Veiculo", "TipoServico", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "Categoria", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculo", "AcessoManobra", c => c.Boolean(nullable: false));
            AddColumn("dbo.Solicitacao", "TipoEmissao", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicitacao", "TipoEmissao");
            DropColumn("dbo.Veiculo", "AcessoManobra");
            DropColumn("dbo.Veiculo", "Categoria");
            DropColumn("dbo.Veiculo", "TipoServico");
        }
    }
}
