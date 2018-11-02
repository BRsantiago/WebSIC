namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterDatesToReceiveNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Credencial", "DataDesativacao", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Credencial", "DataDesativacao", c => c.DateTime(nullable: false));
        }
    }
}
