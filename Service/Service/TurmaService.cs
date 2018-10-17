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
    public class TurmaService : ITurmaService
    {
        private ITurmaRepository turmaRepository;

        public TurmaService(ITurmaRepository repository)
        {
            turmaRepository = repository;
        }

        public ServiceReturn Atualizar(Turma turma)
        {
            try
            {
                turmaRepository.Atualizar(turma);
                turmaRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Turma atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar a turma! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            Turma turma = null;

            try
            {
                turma = turmaRepository.ObterPorId(id);
                turmaRepository.Remover(turma);
                turmaRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Turma deletada com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar a turma! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(Turma turma)
        {
            try
            {
                turmaRepository.Incluir(turma);
                turmaRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Turma cadastrada com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar a turma! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public IList<Turma> Listar()
        {
            IList<Turma> turmas = null;

            try
            {
                return turmaRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return turmas;
        }

        public Turma Obter(int id)
        {
            Turma turma = null;

            try
            {
                turma = turmaRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return turma;
        }

        public IList<Turma> ObterPorCurso(int idCurso)
        {
            IList<Turma> turmas = null;

            try
            {
                turmas = turmaRepository.ObterPorCurso(idCurso);
            }
            catch (Exception ex)
            { }

            return turmas;
        }

        public IList<Turma> ObterProgramadasPorCurso(int idCurso)
        {
            IList<Turma> turmas = null;

            try
            {
                turmas = turmaRepository.ObterProgramadasPorCurso(idCurso);
            }
            catch (Exception ex)
            { }

            return turmas;
        }

        public IList<Turma> ObterRealizadasValidasPorCurso(int idCurso)
        {
            IList<Turma> turmas = null;

            try
            {
                turmas = turmaRepository.ObterRealizadasValidasPorCurso(idCurso);
            }
            catch (Exception ex)
            { }

            return turmas;
        }
    }
}
