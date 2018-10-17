using Entity.DTO;
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
        List<TipoSolicitacao> ObterTodos();
        IList<TipoSolicitacao> Listar();

        TipoSolicitacao Obter(int id);

        ServiceReturn Incluir(TipoSolicitacao tipoSolicitacao);
        ServiceReturn Atualizar(TipoSolicitacao tipoSolicitacao);
        ServiceReturn Excluir(int id);   
    }
}
