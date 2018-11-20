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

        public override void Incluir(CursoSemTurma obj)
        {
            if (!obj.CursoId.HasValue && obj.Curso != null)
                obj.CursoId = obj.Curso.IdCurso;
            if (!obj.PessoaId.HasValue && obj.Pessoa != null)
                obj.PessoaId = obj.Pessoa.IdPessoa;

            base.Incluir(obj);
        }

        public void AtualizarEntidade(CursoSemTurma obj)
        {
            if (obj.Pessoa != null && obj.Pessoa.IdPessoa != 0 && contexto.Entry(obj.Pessoa).State == EntityState.Added)
            {
                contexto.Entry(obj.Pessoa).State = EntityState.Detached;
                obj.Pessoa = contexto.Pessoas.Find(obj.PessoaId);
            }


            if (obj.Curso != null && obj.Curso.IdCurso != 0 && contexto.Entry(obj.Curso).State == EntityState.Added)
            {
                contexto.Entry(obj.Curso).State = EntityState.Detached;
                obj.Curso = contexto.Cursos.Find(obj.CursoId);
            }

            base.Atualizar(obj);
        }


        public CursoSemTurma ObterAgregacaoPorId(int id)
        {
            return this.contexto.CursosSemTurma
                                .Include(cst => cst.Curso)
                                .Include(cst => cst.Pessoa)
                                .Where(cst => cst.IdCursoSemTurma == id)
                                .SingleOrDefault();
        }
    }
}

