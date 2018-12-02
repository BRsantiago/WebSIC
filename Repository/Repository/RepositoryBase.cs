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
    }
}
