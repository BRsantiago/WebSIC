using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public override List<Apolice> ObterTodos()
        {
            return contexto.Apolices.Include("Empresa").Where(a => a.Ativo == true).ToList();
        }

        public override Apolice ObterPorId(int id)
        {
            return contexto.Apolices.Include("Empresa").FirstOrDefault(a => a.IdApolice == id);
        }

        public override void Incluir(Apolice obj)
        {
            contexto.Entry(obj.Empresa).State = EntityState.Modified;
            contexto.Entry(obj).State = EntityState.Added;
            base.Incluir(obj);
        }

        public IList<Apolice> ObterPorEmpresa(int idEmpresa)
        {
            return contexto.Apolices.Include("Empresa").Include("Veiculos").Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.Ativo == true).ToList();
        }

        public Apolice ObterPorNumero(string numero, bool withTracking)
        {
            return withTracking 
                ? contexto.Apolices.Include("Empresa").Include("Veiculos").Where(ap => ap.Numero == numero && ap.Ativo == true).FirstOrDefault()
                : contexto.Apolices.AsNoTracking().Include("Empresa").Include("Veiculos").Where(ap => ap.Numero == numero).FirstOrDefault();
        }

        public IList<Apolice> ObterValidas(int idEmpresa)
        {
            return contexto.Apolices.Include("Empresa").Include("Veiculos").Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.DataValidade > DateTime.Now && ap.Ativo == true).ToList();
        }
    }
}
