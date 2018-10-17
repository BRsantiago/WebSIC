using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository.Repository
{
    public class ContratoRepository : RepositoryBase<Contrato>, IContratoRepository
    {
        public ContratoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override List<Contrato> ObterTodos()
        {
            return contexto.Contratos.Include(c => c.Empresa).Where(c => c.Ativo == true).ToList();
        }

        public override Contrato ObterPorId(int id)
        {
            return contexto.Contratos.Include(c => c.Empresa).Include(c => c.Solicitacoes).FirstOrDefault(c => c.IdContrato == id);
        }

        public override void Incluir(Contrato obj)
        {
            contexto.Entry(obj.Empresa).State = EntityState.Modified;
            contexto.Entry(obj).State = EntityState.Added;
            base.Incluir(obj);
        }

        public IList<Contrato> ObterPorEmpresa(int idEmpresa)
        {
            return contexto.Contratos.Include(c => c.Empresa).Where(c => c.Empresa.IdEmpresa == idEmpresa && c.Ativo == true).ToList();
        }

        public Contrato ObterPorNumero(string numero, bool withTracking)
        {
            return withTracking 
                ? contexto.Contratos.Include(c => c.Empresa).Where(c => c.Numero == numero && c.Ativo == true).FirstOrDefault() 
                : contexto.Contratos.AsNoTracking().Include(c => c.Empresa).Where(c => c.Numero == numero && c.Ativo == true).FirstOrDefault();
        }

        public IList<Contrato> ObterPorPeriodo(int idEmpresa, DateTime inicioVigencia, DateTime finalVigencia)
        {
            return contexto.Contratos.Include(c => c.Empresa).Where(c => c.Empresa.IdEmpresa == idEmpresa && c.InicioVigencia >= inicioVigencia && c.FimVigencia <= finalVigencia && c.Ativo == true).ToList();
        }

        public IList<Contrato> ObterVigentes(int idEmpresa)
        {
            return contexto.Contratos.Include(c => c.Empresa).Where(c => c.Empresa.IdEmpresa == idEmpresa && c.FimVigencia >= DateTime.Now && c.Ativo == true).ToList();
        }

        public void IncluirNovoContrato(Contrato contrato)
        {
            contexto.Entry(contrato.Empresa).State = System.Data.Entity.EntityState.Unchanged;

            contexto.Contratos.Add(contrato);
        }

    }
}
