using Entity.Entities;
using Repository.Context;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Repository.Interface
{
    public class TipoSolicitacaoRepository : RepositoryBase<TipoSolicitacao>, ITipoSolicitacaoRepository
    {
        public TipoSolicitacaoRepository(WebSICContext _contexto)
            : base(_contexto)
        { }
       
    }
}
