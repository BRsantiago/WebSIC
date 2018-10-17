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
    public class TurmaRepository : RepositoryBase<Turma>, ITurmaRepository
    {
        public TurmaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override List<Turma> ObterTodos()
        {
            return contexto.Turmas.Include(t => t.Curso).Where(t => t.Ativo == true).OrderByDescending(t => t.Realizacao).ToList();
        }

        public override Turma ObterPorId(int id)
        {
            return contexto.Turmas.Include(t => t.Curso).Include(t => t.Pessoas).FirstOrDefault(t => t.IdTurma == id);
        }

        public override void Incluir(Turma obj)
        {
            contexto.Entry(obj.Curso).State = EntityState.Modified;
            contexto.Entry(obj).State = EntityState.Added;
            base.Incluir(obj);
        }

        public IList<Turma> ObterPorCurso(int idCurso)
        {
            return contexto.Turmas.Include(t => t.Curso).Where(t => t.Curso.IdCurso == idCurso && t.Ativo == true).OrderByDescending(t => t.Realizacao).ToList();
        }

        public IList<Turma> ObterProgramadasPorCurso(int idCurso)
        {
            return contexto.Turmas.Include(t => t.Curso).Where(t => t.Curso.IdCurso == idCurso && t.Realizacao > DateTime.Now && t.Ativo == true).OrderByDescending(t => t.Realizacao).ToList();
        }

        public IList<Turma> ObterRealizadasValidasPorCurso(int idCurso)
        {
            return contexto.Turmas.Include(t => t.Curso).Where(t => t.Curso.IdCurso == idCurso && t.Realizacao <= DateTime.Now && t.DataValidade > DateTime.Now && t.Ativo == true).OrderByDescending(t => t.Realizacao).ToList();
        }
    }
}
