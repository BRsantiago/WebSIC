using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Context
{
    public class WebSICContext : DbContext
    {
        public WebSICContext() : base(ConfigurationManager.ConnectionStrings["WebSICContext"].ConnectionString)
        {
        }

        public DbSet<Aeroporto> Aeroportos { get; set; }
        public DbSet<Apolice> Apolices { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Credencial> Credenciais { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<PortaoAcesso> PortoesAcesso { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Solicitacao> Solicitacoes { get; set; }
        public DbSet<TipoSolicitacao> TiposSolicitacao { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aeroporto>()
                        .Map(m =>
                        {
                            m.MapInheritedProperties();
                            m.ToTable("Aeroporto");
                        });
            modelBuilder.Entity<Apolice>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Apolice");
            });
            modelBuilder.Entity<Area>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Area");
            });
            modelBuilder.Entity<Cargo>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Cargo");
            });
            modelBuilder.Entity<Contrato>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Contrato");
            });
            modelBuilder.Entity<Credencial>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Credencial");
            });
            modelBuilder.Entity<Curso>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Curso");
            });
            modelBuilder.Entity<Empresa>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Empresa");
            });
            modelBuilder.Entity<Ocorrencia>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Ocorrencia");
            });
            modelBuilder.Entity<Pessoa>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Pessoa");
            });
            modelBuilder.Entity<PortaoAcesso>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("PortaoAcesso");
            });
            modelBuilder.Entity<Schedule>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Schedule");
            });
            modelBuilder.Entity<Solicitacao>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Solicitacao");
            });
            modelBuilder.Entity<TipoSolicitacao>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("TipoSolicitacao");
            });
            modelBuilder.Entity<Turma>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Turma");
            });

            modelBuilder.Entity<Usuario>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Usuario");
            });

            modelBuilder.Entity<Veiculo>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("Veiculo");
            });

            modelBuilder.Entity<CursoSemTurma>().Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("CursoSemTurma");
            });

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<Entity.Entities.TipoEmpresa> TipoEmpresas { get; set; }

        public System.Data.Entity.DbSet<Entity.Entities.TipoCracha> TipoCrachas { get; set; }
    }
}
