namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCurso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Curso", "PermiteDirigirEmAreasRestritas", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Curso", "PermiteDirigirEmAreasRestritas");
        }
    }
}
