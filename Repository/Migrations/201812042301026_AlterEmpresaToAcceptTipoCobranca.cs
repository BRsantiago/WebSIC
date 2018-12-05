namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterEmpresaToAcceptTipoCobranca : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empresa", "TipoCobranca", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empresa", "TipoCobranca", c => c.String());
        }
    }
}
