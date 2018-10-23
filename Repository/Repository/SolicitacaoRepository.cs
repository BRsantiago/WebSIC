﻿using Entity.Entities;
using Repository.Context;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public class SolicitacaoRepository : RepositoryBase<Solicitacao>, ISolicitacaoRepository
    {
        public SolicitacaoRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public void IncluirNovaSolicitacao(Solicitacao solicitacao)
        {
            if (solicitacao.Pessoa != null) contexto.Entry(solicitacao.Pessoa).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Area1 != null) contexto.Entry(solicitacao.Area1).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Area2 != null) contexto.Entry(solicitacao.Area2).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Contrato != null) contexto.Entry(solicitacao.Contrato).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Empresa != null) contexto.Entry(solicitacao.Empresa).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso != null) contexto.Entry(solicitacao.PortaoAcesso).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Schedule != null) contexto.Entry(solicitacao.Schedule).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Veiculo != null) contexto.Entry(solicitacao.Veiculo).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Credencial != null) contexto.Entry(solicitacao.Credencial).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.TipoSolicitacao != null) contexto.Entry(solicitacao.TipoSolicitacao).State = System.Data.Entity.EntityState.Unchanged;

            contexto.Solicitacoes.Add(solicitacao);
        }
    }
}
