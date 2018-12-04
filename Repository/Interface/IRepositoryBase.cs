using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Incluir(TEntity obj);
        TEntity ObterPorId(int id);
        List<TEntity> ObterTodos();
        void Atualizar(TEntity obj);
        void Remover(TEntity obj);
        void Salvar();
        void IniciarTransacao();
        void EncerrarTransacao();
        void DesfazerTransacao();

        List<TEntity> GetDataFromDb(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount);
        Expression<Func<TEntity, bool>> BuildDynamicWhereClause(string searchValue);
    }
}
