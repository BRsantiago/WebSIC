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
    public class ApoliceRepository : RepositoryBase<Apolice>, IApoliceRepository
    {
        public ApoliceRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public IList<Apolice> ObterPorEmpresa(int idEmpresa)
        {
            return contexto.Apolices.Include("Empresa").Include("Veiculos").Where(ap => ap.Empresa.IdEmpresa == idEmpresa).ToList();
        }

        public Apolice ObterPorNumero(string numero, bool withTracking)
        {
            return withTracking 
                ? contexto.Apolices.Include("Empresa").Include("Veiculos").Where(ap => ap.Numero == numero).FirstOrDefault()
                : contexto.Apolices.AsNoTracking().Include("Empresa").Include("Veiculos").Where(ap => ap.Numero == numero).FirstOrDefault();
        }

        public IList<Apolice> ObterValidas(int idEmpresa)
        {
            return contexto.Apolices.Include("Empresa").Include("Veiculos").Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.DataValidade > DateTime.Now).ToList();
        }
    }
}
