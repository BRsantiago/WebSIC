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
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        public CursoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public IList<Curso> ObterPorArea(int idArea)
        {
            return contexto.Cursos.Include("Turmas").Where(c => c.Area.IdArea == idArea).ToList();
        }
    }
}
