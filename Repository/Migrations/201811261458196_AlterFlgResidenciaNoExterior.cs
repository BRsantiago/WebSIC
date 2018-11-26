namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterFlgResidenciaNoExterior : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pessoa", "ObservacaoResidenciaForaDoPaisNosUltimos10Anos", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pessoa", "ObservacaoResidenciaForaDoPaisNosUltimos10Anos", c => c.Boolean(nullable: false));
        }
    }
}
