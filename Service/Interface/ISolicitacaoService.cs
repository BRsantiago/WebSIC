using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISolicitacaoService
    {
        List<Solicitacao> ObterTodos();
        void Salvar(Solicitacao solicitacao);
        void Atualizar(Solicitacao solicitacao);
        Solicitacao ObterPorId(int? id);

        Solicitacao Obter(int id);
        List<Solicitacao> ObterPorVeiculo(int veiculoId);
        void SalvarATIV(Solicitacao solicitacao);
        void AtualizarATIV(Solicitacao solicitacao);
        void AprovarATIV(Solicitacao solicitacao);
        void ExcluirSolicitacao(Solicitacao solicitacao);
    }
}
