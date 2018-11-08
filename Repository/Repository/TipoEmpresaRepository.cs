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
    public class TipoEmpresaRepository : RepositoryBase<TipoEmpresa>, ITipoEmpresaRepository
    {
        public TipoEmpresaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override TipoEmpresa ObterPorId(int id)
        {
            return this.contexto.TipoEmpresas
                                .Where(t => t.IdTipoEmpresa == id)
                                .Include(t => t.TipoCracha)
                                .SingleOrDefault();
        }

        public override List<TipoEmpresa> ObterTodos()
        {
            return this.contexto.TipoEmpresas
                                .Include(t => t.TipoCracha)
                                .ToList();
        }
    }
}
