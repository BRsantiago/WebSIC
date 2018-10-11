using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ContratoRepository : RepositoryBase<Contrato>, IContratoRepository
    {
        public ContratoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public IList<Contrato> ObterPorEmpresa(int idEmpresa)
        {
            return contexto.Contratos.Include("Empresa").Where(c => c.Empresa.IdEmpresa == idEmpresa).ToList();
        }

        public Contrato ObterPorNumero(string numero, bool withTracking)
        {
            return withTracking ? contexto.Contratos.Include("Empresa").Where(c => c.Numero == numero).FirstOrDefault() : contexto.Contratos.AsNoTracking().Include("Empresa").Where(c => c.Numero == numero).FirstOrDefault();
        }

        public IList<Contrato> ObterPorPeriodo(int idEmpresa, DateTime inicioVigencia, DateTime finalVigencia)
        {
            return contexto.Contratos.Include("Empresa").Where(c => c.Empresa.IdEmpresa == idEmpresa && c.InicioVigencia >= inicioVigencia && c.FimVigencia <= finalVigencia).ToList();
        }

        public IList<Contrato> ObterVigentes(int idEmpresa)
        {
            return contexto.Contratos.Include("Empresa").Where(c => c.Empresa.IdEmpresa == idEmpresa && c.FimVigencia >= DateTime.Now).ToList();
        }
    }
}
