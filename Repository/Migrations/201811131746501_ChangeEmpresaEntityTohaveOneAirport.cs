namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeEmpresaEntityTohaveOneAirport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmpresaAeroportoes", "Empresa_IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.EmpresaAeroportoes", "Aeroporto_IdAeroporto", "dbo.Aeroporto");
            DropIndex("dbo.EmpresaAeroportoes", new[] { "Empresa_IdEmpresa" });
            DropIndex("dbo.EmpresaAeroportoes", new[] { "Aeroporto_IdAeroporto" });
            AddColumn("dbo.Empresa", "Aeroporto_IdAeroporto", c => c.Int());
            CreateIndex("dbo.Empresa", "Aeroporto_IdAeroporto");
            AddForeignKey("dbo.Empresa", "Aeroporto_IdAeroporto", "dbo.Aeroporto", "IdAeroporto");
            DropTable("dbo.EmpresaAeroportoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.EmpresaAeroportoes",
                c => new
                    {
                        Empresa_IdEmpresa = c.Int(nullable: false),
                        Aeroporto_IdAeroporto = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Empresa_IdEmpresa, t.Aeroporto_IdAeroporto });
            
            DropForeignKey("dbo.Empresa", "Aeroporto_IdAeroporto", "dbo.Aeroporto");
            DropIndex("dbo.Empresa", new[] { "Aeroporto_IdAeroporto" });
            DropColumn("dbo.Empresa", "Aeroporto_IdAeroporto");
            CreateIndex("dbo.EmpresaAeroportoes", "Aeroporto_IdAeroporto");
            CreateIndex("dbo.EmpresaAeroportoes", "Empresa_IdEmpresa");
            AddForeignKey("dbo.EmpresaAeroportoes", "Aeroporto_IdAeroporto", "dbo.Aeroporto", "IdAeroporto", cascadeDelete: true);
            AddForeignKey("dbo.EmpresaAeroportoes", "Empresa_IdEmpresa", "dbo.Empresa", "IdEmpresa", cascadeDelete: true);
        }
    }
}
