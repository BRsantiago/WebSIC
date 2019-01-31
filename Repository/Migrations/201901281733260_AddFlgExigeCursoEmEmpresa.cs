namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlgExigeCursoEmEmpresa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empresa", "FlgNaoExigeCursoParaAreaRestrita", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empresa", "FlgNaoExigeCursoParaAreaRestrita");
        }
    }
}
