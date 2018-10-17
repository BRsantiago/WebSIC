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
    public class AreaRepository : RepositoryBase<Area>, IAreaRepository
    {
        public AreaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public Area ObterPorSigla(string sigla, bool withTracking)
        {
            return withTracking 
                ? contexto.Areas.Where(a => a.Sigla == sigla).FirstOrDefault() 
                : contexto.Areas.AsNoTracking().Where(a => a.Sigla == sigla).FirstOrDefault();
        }
    }
}
