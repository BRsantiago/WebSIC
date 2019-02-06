using Entity.DTO;
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

        public ServiceReturn Atualizar(Curso curso)
        {
            try
            {
                cursoRepository.Atualizar(curso);
                cursoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Curso atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar o curso! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            Curso curso = null;

            try
            {
                curso = cursoRepository.ObterPorId(id);
                cursoRepository.Remover(curso);
                cursoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Curso deletado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar o curso! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(Curso curso)
        {
            try
            {
                cursoRepository.Incluir(curso);
                cursoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Curso cadastrado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar o curso! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
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

        public List<Curso> ObterTodos()
        {
            return this.cursoRepository.ObterTodos();
        }
    }
}
