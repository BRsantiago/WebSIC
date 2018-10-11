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
    public class SolicitacaoService : ISolicitacaoService
    {
        public ISolicitacaoRepository SolicitacaoRepository;

        public SolicitacaoService(ISolicitacaoRepository _SolicitacaoRepository)
        {
            SolicitacaoRepository = _SolicitacaoRepository;
        }

        public List<Solicitacao> ObterTodos()
        {
            return SolicitacaoRepository.ObterTodos();
        }
    }
}
