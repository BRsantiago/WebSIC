namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterSolicitacaoToAddObservacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solicitacao", "Observacao", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicitacao", "Observacao");
        }
    }
}
