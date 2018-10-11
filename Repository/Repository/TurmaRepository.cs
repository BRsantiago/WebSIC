using Entity.Entities;
using Repository.Context;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public class TurmaRepository : RepositoryBase<Turma>, ITurmaRepository
    {
        public TurmaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public IList<Turma> ObterPorCurso(int idCurso)
        {
            return contexto.Turmas.Include("Curso").Where(t => t.Curso.IdCurso == idCurso).OrderByDescending(t => t.Realizacao).ToList();
        }

        public IList<Turma> ObterProgramadasPorCurso(int idCurso)
        {
            return contexto.Turmas.Include("Curso").Where(t => t.Curso.IdCurso == idCurso && t.Realizacao > DateTime.Now).OrderByDescending(t => t.Realizacao).ToList();
        }

        public IList<Turma> ObterRealizadasValidasPorCurso(int idCurso)
        {
            return contexto.Turmas.Include("Curso").Where(t => t.Curso.IdCurso == idCurso && t.Realizacao <= DateTime.Now && t.DataValidade > DateTime.Now).OrderByDescending(t => t.Realizacao).ToList();
        }
    }
}
