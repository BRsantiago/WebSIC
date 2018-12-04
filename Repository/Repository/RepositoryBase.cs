using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Expressions;
using LinqKit;
using Repository.Extensions;

namespace Repository.Repository
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        public WebSICContext contexto;
        private DbContextTransaction transacao;

        public RepositoryBase(WebSICContext _contexto)
        {
            contexto = _contexto;
        }

        public RepositoryBase() { }

        public virtual void Incluir(TEntity obj)
        {
            contexto.Set<TEntity>().Add(obj);
        }

        public virtual TEntity ObterPorId(int id)
        {
            return contexto.Set<TEntity>().Find(id);
        }

        public virtual List<TEntity> ObterTodos()
        {
            return contexto.Set<TEntity>().ToList();
        }

        public virtual void Atualizar(TEntity obj)
        {
            contexto.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void Remover(TEntity obj)
        {
            contexto.Set<TEntity>().Remove(obj);
        }

        public void Dispose()
        {
            contexto.Dispose();
        }

        public void Salvar()
        {
            contexto.SaveChanges();
        }

        public void IniciarTransacao()
        {
            transacao = contexto.Database.BeginTransaction();
        }

        public void EncerrarTransacao()
        {
            transacao.Commit();
        }

        public void DesfazerTransacao()
        {
            transacao.Rollback();
        }

        public List<TEntity> GetDataFromDb(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            var whereClause = BuildDynamicWhereClause(searchBy);

            var result = contexto.Set<TEntity>()
                .AsExpandable()
                .Where(whereClause)
                .OrderBy(sortBy, sortDir)
                .Skip(skip)
                .Take(take)
                .ToList();

            filteredResultsCount = contexto.Set<TEntity>()
                .AsExpandable()
                .Where(whereClause)
                .Count();

            totalResultsCount = contexto.Set<TEntity>().Count();

            return result;
        }

        public Expression<Func<TEntity, bool>> BuildDynamicWhereClause(string searchValue)
        {
            // simple method to dynamically plugin a where clause
            var predicate = PredicateBuilder.New<TEntity>(true); // true -where(true) return all
            if (!String.IsNullOrWhiteSpace(searchValue))
                predicate = ConfigureFilter(predicate, searchValue);

            return predicate;
        }

        public virtual ExpressionStarter<TEntity> ConfigureFilter(ExpressionStarter<TEntity> predicate, string searchValue)
        {
            return predicate;
        }
    }
}
