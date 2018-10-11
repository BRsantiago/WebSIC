namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteVeiculo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Veiculo", "Marca", c => c.String());
            AddColumn("dbo.Veiculo", "Modelo", c => c.String());
            AddColumn("dbo.Veiculo", "AnoFabricacao", c => c.String());
            AddColumn("dbo.Veiculo", "AnoModelo", c => c.String());
            AlterColumn("dbo.Veiculo", "Placa", c => c.String(nullable: false));
            AlterColumn("dbo.Veiculo", "Chassi", c => c.String(nullable: false));
            DropColumn("dbo.Veiculo", "Descricao");
            DropColumn("dbo.Veiculo", "Ano");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Veiculo", "Ano", c => c.String());
            AddColumn("dbo.Veiculo", "Descricao", c => c.String());
            AlterColumn("dbo.Veiculo", "Chassi", c => c.String());
            AlterColumn("dbo.Veiculo", "Placa", c => c.String());
            DropColumn("dbo.Veiculo", "AnoModelo");
            DropColumn("dbo.Veiculo", "AnoFabricacao");
            DropColumn("dbo.Veiculo", "Modelo");
            DropColumn("dbo.Veiculo", "Marca");
        }
    }
}
