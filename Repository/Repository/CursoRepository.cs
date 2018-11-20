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
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        public CursoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override void Atualizar(Curso obj)
        {
            base.Atualizar(obj);
        }

        public void AtualizarListaPorPessoa(int pessoaId, List<Curso> cursos, string username)
        {
            List<CursoSemTurma> cst = new List<CursoSemTurma>();

            Pessoa pessoa = contexto.Pessoas
                .Include(p => p.Cursos)
                .FirstOrDefault(p => p.IdPessoa == pessoaId);

            foreach (Curso curso in cursos)
                cst.Add(new CursoSemTurma() {
                    Curso = curso,
                    CursoId = curso.IdCurso,
                    Pessoa = pessoa,
                    PessoaId = pessoa.IdPessoa,
                    Criacao = DateTime.Now,
                    Atualizacao = DateTime.Now,
                    Criador = username,
                    Atualizador = username,
                    Ativo = true,
                    DataValidade = DateTime.Now.AddDays(curso.Validade)
                });

            var deletedCourses = pessoa.Cursos.Except(cst).ToList();
            var addedCourses = cst.Except(pessoa.Cursos).ToList();

            deletedCourses.ForEach(c => pessoa.Cursos.Remove(c));

            foreach (CursoSemTurma added in addedCourses)
            {
                if (contexto.Entry(added).State == EntityState.Detached)
                {
                    contexto.CursosSemTurma.Attach(added);
                    contexto.Entry(added).State = EntityState.Added;
                    contexto.Entry(added.Curso).State = EntityState.Modified;
                    contexto.Entry(added.Pessoa).State = EntityState.Modified;
                }
            }
        }

        public override List<Curso> ObterTodos()
        {
            return contexto.Cursos
                .Include(c => c.Areas)
                .Include(c => c.Turmas)
                .Include(c => c.CursosSemTurma)
                .Where(c => c.Ativo == true)
                .ToList();
        }

        public override Curso ObterPorId(int id)
        {
            return 
                contexto.Cursos
                    .Include(c => c.Areas)
                    .Include(c => c.Turmas)
                    .Include(c => c.CursosSemTurma)
                    .FirstOrDefault(c => c.IdCurso == id);
        }

        public override void Incluir(Curso obj)
        {
            contexto.Entry(obj).State = EntityState.Added;
            base.Incluir(obj);
        }

        public IList<Curso> ObterPorArea(int idArea)
        {
            return 
                contexto.Cursos
                    .Include(c => c.Areas)
                    .Include(c => c.Turmas)
                    .Include(c => c.CursosSemTurma)
                    .Where(c => c.Areas.Any(a => a.IdArea == idArea) && c.Ativo == true)
                    .ToList();
        }

        public IList<Curso> ObterCursosRealizadosComValidadePorIdPessoa(int idPessoa)
        {
            return 
                contexto.Cursos
                    .Include(c => c.Areas)
                    .Include(c => c.Turmas)
                    .Include(c => c.CursosSemTurma)
                    .Where(c => 
                        c.Turmas.Any(t => t.Pessoas.Any(p => p.IdPessoa == idPessoa) && t.DataValidade > DateTime.Now) ||
                        c.CursosSemTurma.Any(t => t.Pessoa.IdPessoa == idPessoa && t.DataValidade > DateTime.Now)
                     ) 
                     .ToList();
        }
    }
}
