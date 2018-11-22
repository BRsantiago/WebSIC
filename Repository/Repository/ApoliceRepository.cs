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
            return contexto.Apolices.Include(a => a.Empresa).Where(a => a.Ativo == true).ToList();
        }

        public override Apolice ObterPorId(int id)
        {
            return contexto.Apolices.Include(a => a.Empresa).Include(a => a.Veiculos).FirstOrDefault(a => a.IdApolice == id);
        }

        public override void Incluir(Apolice obj)
        {
            #region
            //if (contexto.Entry(obj.Empresa).State == EntityState.Detached)
            //    contexto.Empresas.Attach(obj.Empresa);
            //contexto.Entry(obj.Empresa).State = EntityState.Modified;
            //contexto.Entry(obj).State = EntityState.Added;
            #endregion

            base.Incluir(obj);
        }

        public override void Atualizar(Apolice obj)
        {
            if (contexto.Entry(obj).State == EntityState.Detached)
            {
                var existingtObj = contexto.Apolices.FirstOrDefault(a => a.IdApolice == obj.IdApolice);
                contexto.Entry(existingtObj).CurrentValues.SetValues(obj);
            }

            #region
            //if (obj.Empresa != null)
            //    contexto.Entry(obj.Empresa).State = EntityState.Unchanged;

            //base.Atualizar(obj);
            #endregion
        }

        public IList<Apolice> ObterPorEmpresa(int idEmpresa, bool withTracking)
        {
            return withTracking
                ? contexto.Apolices.Include(a => a.Empresa).Include(a => a.Veiculos).Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.Ativo == true).ToList()
                : contexto.Apolices.AsNoTracking().Include(a => a.Empresa).Include(a => a.Veiculos).Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.Ativo == true).ToList();
        }

        public Apolice ObterPorNumero(string numero, bool withTracking)
        {
            return withTracking 
                ? contexto.Apolices.Include(a => a.Empresa).Include(a => a.Veiculos).Where(ap => ap.Numero == numero && ap.Ativo == true).FirstOrDefault()
                : contexto.Apolices.AsNoTracking().Include(a => a.Empresa).Include(a => a.Veiculos).Where(ap => ap.Numero == numero && ap.Ativo == true).FirstOrDefault();
        }

        public IList<Apolice> ObterValidas(int idEmpresa, bool withTracking)
        {
            return withTracking
                ? contexto.Apolices.Include(a => a.Empresa).Include(a => a.Veiculos).Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.DataValidade > DateTime.Now && ap.Ativo == true).ToList()
                : contexto.Apolices.AsNoTracking().Include(a => a.Empresa).Include(a => a.Veiculos).Where(ap => ap.Empresa.IdEmpresa == idEmpresa && ap.DataValidade > DateTime.Now && ap.Ativo == true).ToList();
        }
    }
}
