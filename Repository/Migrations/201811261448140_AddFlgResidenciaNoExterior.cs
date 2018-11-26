namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlgResidenciaNoExterior : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pessoa", "FlgResidenciaForaDoPaisNosUltimos10Anos", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pessoa", "ObservacaoResidenciaForaDoPaisNosUltimos10Anos", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pessoa", "ObservacaoResidenciaForaDoPaisNosUltimos10Anos");
            DropColumn("dbo.Pessoa", "FlgResidenciaForaDoPaisNosUltimos10Anos");
        }
    }
}
