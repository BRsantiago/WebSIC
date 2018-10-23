using Entity.Entities;
using Repository.Context;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository.Interface
{
    public class VeiculoRepository : RepositoryBase<Veiculo>, IVeiculoRepository
    {
        public VeiculoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override List<Veiculo> ObterTodos()
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Ativo == true).ToList();
        }

        public override Veiculo ObterPorId(int id)
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).FirstOrDefault(v => v.IdVeiculo == id);
        }

        public override void Incluir(Veiculo obj)
        {
            if (contexto.Entry(obj.Empresa).State == EntityState.Detached)
                contexto.Empresas.Attach(obj.Empresa);
            contexto.Entry(obj.Empresa).State = EntityState.Modified;

            if (contexto.Entry(obj.Apolice).State == EntityState.Detached)
                contexto.Apolices.Attach(obj.Apolice);
            contexto.Entry(obj.Apolice).State = EntityState.Modified;
            
            contexto.Entry(obj).State = EntityState.Added;
            base.Incluir(obj);
        }

        public override void Atualizar(Veiculo obj)
        {
            if (contexto.Entry(obj.Empresa).State == EntityState.Detached)
                contexto.Empresas.Attach(obj.Empresa);
            contexto.Entry(obj.Empresa).State = EntityState.Modified;

            if (contexto.Entry(obj.Apolice).State == EntityState.Detached)
                contexto.Apolices.Attach(obj.Apolice);
            contexto.Entry(obj.Apolice).State = EntityState.Modified;

            base.Atualizar(obj);
        }

        public IList<Veiculo> ObterPorAno(string ano)
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => (v.AnoFabricacao == ano || v.AnoModelo == ano) && v.Ativo == true).ToList();
        }

        public IList<Veiculo> ObterPorApolice(int idApolice)
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Apolice.IdApolice == idApolice).ToList();
        }

        public Veiculo ObterPorChassi(string chassi, bool withTracking)
        {
            return withTracking
                ? contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Chassi == chassi && v.Ativo == true).FirstOrDefault()
                : contexto.Veiculos.AsNoTracking().Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Chassi == chassi && v.Ativo == true).FirstOrDefault();
        }

        public IList<Veiculo> ObterPorEmpresa(int idEmpresa)
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Empresa.IdEmpresa == idEmpresa && v.Ativo == true).ToList();
        }

        public IList<Veiculo> ObterPorMarca(string marca)
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Marca == marca && v.Ativo == true).ToList();
        }

        public IList<Veiculo> ObterPorModelo(string modelo)
        {
            return contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Modelo == modelo && v.Ativo == true).ToList();
        }

        public Veiculo ObterPorPlaca(string placa, bool withTracking)
        {
            return withTracking
                ? contexto.Veiculos.Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Placa == placa && v.Ativo == true).FirstOrDefault()
                : contexto.Veiculos.AsNoTracking().Include(v => v.Empresa).Include(v => v.Apolice).Where(v => v.Placa == placa && v.Ativo == true).FirstOrDefault();
        }
    }
}
