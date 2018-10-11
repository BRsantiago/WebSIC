using Entity.Entities;
using Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TipoCrachaRepository : RepositoryBase<TipoCracha>
    {
        public TipoCrachaRepository(WebSICContext _contexto)
           : base(_contexto)
        {
        }
    }
}
