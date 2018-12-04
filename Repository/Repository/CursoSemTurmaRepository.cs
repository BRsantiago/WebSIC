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
    public class CursoSemTurmaRepository : RepositoryBase<CursoSemTurma>, ICursoSemTurmaRepository
    {
        public CursoSemTurmaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override void Incluir(CursoSemTurma cst)
        {
            //contexto.Entry(cst.Curso).State = System.Data.Entity.EntityState.Unchanged;
            //contexto.Entry(cst.Pessoa).State = System.Data.Entity.EntityState.Unchanged;

            contexto.CursosSemTurma.Add(cst);
        }

        public override void Atualizar(CursoSemTurma cst)
        {
            //contexto.Entry(cst.Curso).State = System.Data.Entity.EntityState.Detached;
            //contexto.Entry(cst.Pessoa).State = System.Data.Entity.EntityState.Detached;

            contexto.Entry(cst).State = EntityState.Modified;
        }
        
        public override CursoSemTurma ObterPorId(int id)
        {
            return this.contexto.CursosSemTurma
                                .Include(cst => cst.Curso)
                                .Include(cst => cst.Pessoa)
                                .Where(cst => cst.IdCursoSemTurma == id)
                                .SingleOrDefault();
        }
    }
}

