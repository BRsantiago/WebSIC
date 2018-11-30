namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnexoCertidoesSolicitacao : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Solicitacao", "CertTRFFilePath", c => c.String());
            AddColumn("dbo.Solicitacao", "CertAntCrimPFFilePath", c => c.String());
            AddColumn("dbo.Solicitacao", "CertAntCrimPCFilePath", c => c.String());
            AddColumn("dbo.Solicitacao", "CertTJBAFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Solicitacao", "CertTJBAFilePath");
            DropColumn("dbo.Solicitacao", "CertAntCrimPCFilePath");
            DropColumn("dbo.Solicitacao", "CertAntCrimPFFilePath");
            DropColumn("dbo.Solicitacao", "CertTRFFilePath");
        }
    }
}
