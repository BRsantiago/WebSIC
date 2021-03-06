﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface ISolicitacaoRepository : IRepositoryBase<Solicitacao>
    {
        void IncluirNovaSolicitacao(Solicitacao solicitacao);
        List<Solicitacao> ObterPorVeiculo(int veiculoId);

        void AtualizarAnexos(Solicitacao solicitacao);
        Solicitacao ObterUltimaSolicitacaoPorIdCredencial(int idCredencial);
    }
}
