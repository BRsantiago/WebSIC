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

        public TipoSolicitacao Atualizar(TipoSolicitacao tipoSolicitacao)
        {
            try
            {
                tipoSolicitacaoRepository.Atualizar(tipoSolicitacao);
                tipoSolicitacaoRepository.Salvar();

                return tipoSolicitacao;
            }
            catch (Exception ex)
            { }

            return null;
        }

        public int Excluir(int id)
        {
            TipoSolicitacao tipoSolicitacao = null;

            try
            {
                tipoSolicitacao = tipoSolicitacaoRepository.ObterPorId(id);
                tipoSolicitacaoRepository.Remover(tipoSolicitacao);
                tipoSolicitacaoRepository.Salvar();

                return id;
            }
            catch (Exception ex)
            { }

            return 0;
        }

        public TipoSolicitacao Incluir(TipoSolicitacao tipoSolicitacao)
        {
            try
            {
                tipoSolicitacaoRepository.Incluir(tipoSolicitacao);
                tipoSolicitacaoRepository.Salvar();

                return tipoSolicitacao;
            }
            catch (Exception ex)
            { }

            return null;
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
