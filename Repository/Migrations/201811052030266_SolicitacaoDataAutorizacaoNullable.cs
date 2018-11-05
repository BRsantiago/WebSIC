namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SolicitacaoDataAutorizacaoNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Solicitacao", "DataAutorizacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Solicitacao", "DataAutorizacao", c => c.DateTime(nullable: false));
        }
    }
}
