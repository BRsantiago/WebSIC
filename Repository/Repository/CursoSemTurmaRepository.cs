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
    public class CursoSemTurmaRepository : RepositoryBase<CursoSemTurma>, ICursoSemTurmaRepository
    {
        public WebSICContext contexto;

        public CursoSemTurmaRepository(WebSICContext _contexto)
        {
            contexto = _contexto;
        }

        public void Incluir(CursoSemTurma cst)
        {
            contexto.Entry(cst.Curso).State = System.Data.Entity.EntityState.Unchanged;
            contexto.Entry(cst.Pessoa).State = System.Data.Entity.EntityState.Unchanged;

            contexto.CursosSemTurma.Add(cst);
        }
    }
}

