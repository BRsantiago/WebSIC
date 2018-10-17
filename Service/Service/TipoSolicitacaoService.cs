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
    public class TipoSolicitacaoService : ITipoSolicitacaoService
    {
        ITipoSolicitacaoRepository tipoSolicitacaoRepository;

        public TipoSolicitacaoService(ITipoSolicitacaoRepository repository)
        {
            tipoSolicitacaoRepository = repository;
        }

        public ServiceReturn Atualizar(TipoSolicitacao tipoSolicitacao)
        {
            try
            {
                tipoSolicitacaoRepository.Atualizar(tipoSolicitacao);
                tipoSolicitacaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Tipo de solicitação atualizado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao atualizar o tipo de solicitação! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Excluir(int id)
        {
            TipoSolicitacao tipoSolicitacao = null;

            try
            {
                tipoSolicitacao = tipoSolicitacaoRepository.ObterPorId(id);
                tipoSolicitacaoRepository.Remover(tipoSolicitacao);
                tipoSolicitacaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Tipo de solicitação deletado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao deletar o tipo de solicitação! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public ServiceReturn Incluir(TipoSolicitacao tipoSolicitacao)
        {
            try
            {
                tipoSolicitacaoRepository.Incluir(tipoSolicitacao);
                tipoSolicitacaoRepository.Salvar();

                return new ServiceReturn() { success = true, title = "Sucesso", message = "Tipo de solicitação cadastrado com sucesso!" };
            }
            catch (Exception ex)
            {
                return new ServiceReturn() { success = false, title = "Erro", message = string.Format("Um erro do tipo {0} foi disparado ao cadastrar o tipo de solicitação! Mensagem: {1}", ex.GetType(), ex.Message) };
            }
        }

        public IList<TipoSolicitacao> Listar()
        {
            IList<TipoSolicitacao> tipoSolicitacaos = null;

            try
            {
                return tipoSolicitacaoRepository.ObterTodos();
            }
            catch (Exception ex)
            { }

            return tipoSolicitacaos;
        }

        public TipoSolicitacao Obter(int id)
        {
            TipoSolicitacao tipoSolicitacao = null;

            try
            {
                tipoSolicitacao = tipoSolicitacaoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            { }

            return tipoSolicitacao;
        }
        public List<TipoSolicitacao> ObterTodos()
        {
            return tipoSolicitacaoRepository.ObterTodos();
        }
    }
}
