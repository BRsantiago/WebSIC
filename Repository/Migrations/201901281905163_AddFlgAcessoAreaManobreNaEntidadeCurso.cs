namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlgAcessoAreaManobreNaEntidadeCurso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Curso", "FlgAcessoAreaManobra", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Curso", "FlgAcessoAreaManobra");
        }
    }
}
