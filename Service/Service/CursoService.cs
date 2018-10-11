using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class CursoService : ICursoService
    {
        private ICursoRepository cursoRepository;

        public CursoService(ICursoRepository repository)
        {
            cursoRepository = repository;
        }

        public Curso Atualizar(Curso curso)
        {
            try
            {
                cursoRepository.Atualizar(curso);
                cursoRepository.Salvar();

                return curso;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            Curso curso = null;

            try
            {
                curso = cursoRepository.ObterPorId(id);
                cursoRepository.Remover(curso);
                cursoRepository.Salvar();

                return id;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public Curso Incluir(Curso curso)
        {
            try
            {
                cursoRepository.Incluir(curso);
                cursoRepository.Salvar();

                return curso;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public IList<Curso> Listar()
        {
            IList<Curso> cursos = null;

            try
            {
                return cursoRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return cursos;
        }

        public Curso Obter(int id)
        {
            Curso curso = null;

            try
            {
                curso = cursoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return curso;
        }

        public IList<Curso> ObterPorArea(int idArea)
        {
            IList<Curso> cursos = null;

            try
            {
                return cursoRepository.ObterPorArea(idArea);
            }
            catch (Exception ex)
            { }

            return cursos;
        }
    }
}
