using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITipoSolicitacaoService
    {
        IList<TipoSolicitacao> Listar();
        TipoSolicitacao Obter(int id);
        TipoSolicitacao Incluir(TipoSolicitacao tipoSolicitacao);
        TipoSolicitacao Atualizar(TipoSolicitacao tipoSolicitacao);
        int Excluir(int id);
    }
}
