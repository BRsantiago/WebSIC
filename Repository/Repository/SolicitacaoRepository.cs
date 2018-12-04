using Entity.Entities;
using Repository.Context;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

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
            if (solicitacao.Pessoa != null)
            {
                solicitacao.Pessoa.Turmas.ToList()
                                     .ForEach(t =>
                                     {
                                         if (t.IdTurma == 0)
                                             contexto.Turmas.Add(t);
                                         else
                                             contexto.Entry(t).State = System.Data.Entity.EntityState.Modified;
                                     });

                solicitacao.Pessoa.Curso.ToList()
                                        .ForEach(c =>
                                        {
                                            if (c.IdCursoSemTurma == 0)
                                            {
                                                if (contexto.Entry(c.Curso).State != System.Data.Entity.EntityState.Detached)
                                                    contexto.Entry(c.Curso).State = System.Data.Entity.EntityState.Unchanged;
                                                contexto.CursosSemTurma.Add(c);
                                            }
                                            else
                                            {
                                                if (contexto.Entry(c.Curso).State != System.Data.Entity.EntityState.Detached)
                                                {
                                                    contexto.Entry(c.Curso).State = System.Data.Entity.EntityState.Unchanged;
                                                    contexto.Entry(c).State = System.Data.Entity.EntityState.Unchanged;
                                                }
                                            }
                                        });

                solicitacao.Pessoa.Solicitacaos.ToList().ForEach(s =>
                {
                    if (s.IdSolicitacao != 0)
                    {
                        contexto.Entry(s).State = System.Data.Entity.EntityState.Modified;
                    }

                });


                contexto.Entry(solicitacao.Pessoa).State = System.Data.Entity.EntityState.Unchanged;
            }

            if (solicitacao.Area1 != null) contexto.Entry(solicitacao.Area1).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Area2 != null) contexto.Entry(solicitacao.Area2).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Contrato != null) contexto.Entry(solicitacao.Contrato).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Empresa != null) contexto.Entry(solicitacao.Empresa).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso1 != null) contexto.Entry(solicitacao.PortaoAcesso1).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso2 != null) contexto.Entry(solicitacao.PortaoAcesso2).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso3 != null) contexto.Entry(solicitacao.PortaoAcesso3).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Schedule != null) contexto.Entry(solicitacao.Schedule).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Veiculo != null) contexto.Entry(solicitacao.Veiculo).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Credencial != null) contexto.Entry(solicitacao.Credencial).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.TipoSolicitacao != null) contexto.Entry(solicitacao.TipoSolicitacao).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Cargo != null) contexto.Entry(solicitacao.Cargo).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Aeroporto != null) contexto.Entry(solicitacao.Aeroporto).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.RamoAtividade != null) contexto.Entry(solicitacao.RamoAtividade).State = System.Data.Entity.EntityState.Unchanged;

            contexto.Solicitacoes.Add(solicitacao);
        }

        public override void Atualizar(Solicitacao solicitacao)
        {
            contexto.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;

            if (solicitacao.Pessoa != null)
            {
                solicitacao.Pessoa.Turmas.ToList()
                                     .ForEach(t =>
                                     {
                                         if (t.IdTurma == 0)
                                             contexto.Turmas.Add(t);
                                         else
                                             contexto.Entry(t).State = System.Data.Entity.EntityState.Modified;
                                     });

                solicitacao.Pessoa.Curso.ToList()
                                        .ForEach(c =>
                                        {
                                            if (c.IdCursoSemTurma == 0)
                                            {
                                                contexto.Entry(c.Curso).State = System.Data.Entity.EntityState.Unchanged;
                                                contexto.CursosSemTurma.Add(c);
                                            }
                                            else
                                            {
                                                contexto.Entry(c.Curso).State = System.Data.Entity.EntityState.Unchanged;
                                                contexto.Entry(c).State = System.Data.Entity.EntityState.Modified;
                                            }
                                        });

                contexto.Entry(solicitacao.Pessoa).State = System.Data.Entity.EntityState.Unchanged;
            }
            if (solicitacao.Area1 != null) contexto.Entry(solicitacao.Area1).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Area2 != null) contexto.Entry(solicitacao.Area2).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Contrato != null) contexto.Entry(solicitacao.Contrato).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Empresa != null) contexto.Entry(solicitacao.Empresa).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso1 != null) contexto.Entry(solicitacao.PortaoAcesso1).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso2 != null) contexto.Entry(solicitacao.PortaoAcesso2).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.PortaoAcesso3 != null) contexto.Entry(solicitacao.PortaoAcesso3).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Schedule != null) contexto.Entry(solicitacao.Schedule).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Veiculo != null) contexto.Entry(solicitacao.Veiculo).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Credencial != null) contexto.Entry(solicitacao.Credencial).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.TipoSolicitacao != null) contexto.Entry(solicitacao.TipoSolicitacao).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Cargo != null) contexto.Entry(solicitacao.Cargo).State = System.Data.Entity.EntityState.Unchanged;
            if (solicitacao.Aeroporto != null) contexto.Entry(solicitacao.Aeroporto).State = System.Data.Entity.EntityState.Unchanged;


        }

        public void AtualizarAnexos(Solicitacao solicitacao)
        {
            if (contexto.Entry(solicitacao).State == EntityState.Detached)
            {
                var existingtObj = contexto.Solicitacoes.Find(solicitacao.IdSolicitacao);
                contexto.Entry(existingtObj).CurrentValues.SetValues(solicitacao);
            }
        }

        public List<Solicitacao> ObterPorVeiculo(int veiculoId)
        {
            return contexto.Solicitacoes
                .Include(s => s.Veiculo)
                .Include(s => s.Veiculo.Apolice)
                .Include(s => s.Empresa)
                .Include(s => s.Contrato)
                .Include(s => s.Area1)
                .Include(s => s.Area2)
                .Include(s => s.PortaoAcesso1)
                .Include(s => s.PortaoAcesso2)
                .Include(s => s.PortaoAcesso3)
                .Include(s => s.TipoSolicitacao)
                .Where(s => s.Veiculo.IdVeiculo == veiculoId)
                .ToList();
        }

        public override Solicitacao ObterPorId(int id)
        {
            return contexto.Solicitacoes
                           .Include(s => s.Aeroporto)
                           .Include(s => s.Veiculo)
                           .Include(s => s.Veiculo.Apolice)
                           .Include(s => s.Empresa)
                           .Include(s => s.Contrato)
                           .Include(s => s.Area1)
                           .Include(s => s.Area2)
                           .Include(s => s.PortaoAcesso1)
                           .Include(s => s.PortaoAcesso2)
                           .Include(s => s.PortaoAcesso3)
                           .Include(s => s.TipoSolicitacao)
                           .Include(s => s.Cargo)
                           .Include(s => s.Pessoa)
                           .Include(s => s.Credencial.Solicitacoes)
                           .SingleOrDefault(s => s.IdSolicitacao == id);
        }
    }
}
