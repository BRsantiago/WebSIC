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
    public class TipoCrachaRepository : RepositoryBase<TipoCracha>, ITipoCrachaRepository
    {
        public TipoCrachaRepository(WebSICContext _contexto)
           : base(_contexto)
        {
        }

        public TipoCracha ObterTipoCrachaTemporario()
        {
            return this.contexto.TipoCrachas
                                .Where(tp => tp.FlgCrachaTemporario)
                                .SingleOrDefault();
        }
    }
}
