namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterDatesAreNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Credencial", "DataVencimento", c => c.DateTime());
            AlterColumn("dbo.Credencial", "DataExpedicao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Credencial", "DataExpedicao", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Credencial", "DataVencimento", c => c.DateTime(nullable: false));
        }
    }
}
