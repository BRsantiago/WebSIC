using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LinqKit;

namespace Repository.Repository
{
    public class EmpresaRepository : RepositoryBase<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(WebSICContext _contexto) : base(_contexto)
        {
        }

        public void Incluir(Empresa empresa)
        {
            contexto.Entry(empresa.TipoEmpresa).State = System.Data.Entity.EntityState.Unchanged;
            contexto.Entry(empresa.Aeroporto).State = System.Data.Entity.EntityState.Unchanged;

            contexto.Empresas.Add(empresa);
        }

        public Empresa ObterPorId(int id)
        {
            return contexto.Empresas
                           .Include(e => e.TipoEmpresa)
                           .Include(e => e.Aeroporto)
                           .Include(e => e.Apolices)
                           .Include(e => e.Credenciais)
                           .Include(e => e.Solicitacoes)
                           .Include(e => e.Veiculos)
                           .Where(e => e.IdEmpresa == id)
                           .SingleOrDefault();
        }

        public List<Empresa> ObterTodos()
        {
            return contexto.Set<Empresa>().ToList();
        }

        public virtual void Atualizar(Empresa empresa)
        {
            contexto.Entry(empresa.TipoEmpresa).State = System.Data.Entity.EntityState.Detached;

            //foreach (var aeroporto in empresa.Aeroportos)
            //{
            //    contexto.Entry(aeroporto).State = System.Data.Entity.EntityState.Unchanged;
            //}

            contexto.Entry(empresa).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Remover(Empresa obj)
        {
            contexto.Set<Empresa>().Remove(obj);
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public void Salvar()
        {
            contexto.SaveChanges();
        }

        public List<Empresa> ObterPorAeroporto(int aeroportoId)
        {
            return contexto.Empresas
                           .AsNoTracking()
                           .Where(e => e.Aeroporto.IdAeroporto == aeroportoId)
                           .ToList();
        }

        public List<Empresa> GetDataFromDatabase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            return base.GetDataFromDb(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
        }

        public override ExpressionStarter<Empresa> ConfigureFilter(ExpressionStarter<Empresa> predicate, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToUpper();

                predicate = predicate.Or(e => e.RazaoSocial.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.NomeFantasia.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.CGC.ToUpper().Contains(searchValue));
            }

            return predicate;
        }
    }
}
