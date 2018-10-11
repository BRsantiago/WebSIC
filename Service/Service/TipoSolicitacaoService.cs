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
        public ITipoSolicitacaoRepository TipoSolicitacaoRepository;

        public TipoSolicitacaoService(ITipoSolicitacaoRepository _TipoSolicitacaoRepository)
        {
            TipoSolicitacaoRepository = _TipoSolicitacaoRepository;
        }

        public List<TipoSolicitacao> ObterTodos()
        {
            return TipoSolicitacaoRepository.ObterTodos();
        }
    }
}
