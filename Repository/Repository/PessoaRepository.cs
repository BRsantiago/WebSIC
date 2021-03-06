﻿using Entity.Entities;
using Repository.Context;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LinqKit;

namespace Repository.Repository
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(WebSICContext _contexto)
            : base(_contexto)
        {
        }

        public override Pessoa ObterPorId(int idPessoa)
        {
            return contexto.Pessoas
                           .Include(p => p.Empresas)
                           .Include(p => p.Solicitacaos)
                           .Include(p => p.Solicitacaos.Select(s => s.Aeroporto))
                           .Include(p => p.Solicitacaos.Select(s => s.TipoSolicitacao))
                           .Include(p => p.Solicitacaos.Select(s => s.Empresa))
                           .Include(p => p.Solicitacaos.Select(s => s.Contrato))
                           .Include(p => p.Solicitacaos.Select(s => s.Area1))
                           .Include(p => p.Solicitacaos.Select(s => s.Area2))
                           .Include(p => p.Curso.Select(c => c.Curso))
                           .Include(p => p.Turmas.Select(c => c.Curso))
                           .Include(p => p.Credenciais)
                           .Where(p => p.IdPessoa == idPessoa).SingleOrDefault();
        }

        public Pessoa ObterPorIdSemAgregacao(int idPessoa)
        {
            return contexto.Pessoas
                           .Where(p => p.IdPessoa == idPessoa).SingleOrDefault();
        }

        public Pessoa ObterPorCPF(string cpf)
        {
            return this.contexto.Pessoas.Where(p => p.CPF.Contains(cpf)).SingleOrDefault();
        }

        public List<Pessoa> ObterPorEmpresa(int idEmpresa)
        {
            return this.contexto.Pessoas.Where(p => p.Empresas.Any(e => e.IdEmpresa == idEmpresa)).ToList();
        }

        public void IncluirNovoRepresentante(Pessoa representante)
        {
            representante.Empresas.ToList().ForEach(empresa => contexto.Entry(empresa).State = System.Data.Entity.EntityState.Unchanged);

            contexto.Pessoas.Add(representante);
        }

        public void AtualizarRepresentante(Pessoa pessoa)
        {
            if (pessoa.Empresas != null) pessoa.Empresas.ToList().ForEach(empresa => contexto.Entry(empresa).State = System.Data.Entity.EntityState.Unchanged);

            if (pessoa.Turmas != null)
            {
                pessoa.Turmas.ToList()
                        .ForEach(t =>
                        {
                            if (t.IdTurma == 0)
                                contexto.Turmas.Add(t);
                            else
                                contexto.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        });
            }

            if (pessoa.Curso != null)
            {
                pessoa.Curso.ToList()
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
            }

            contexto.Entry(pessoa).State = System.Data.Entity.EntityState.Modified;
        }

        public void IncluirNovaPessoa(Pessoa pessoa)
        {
            if (pessoa.Turmas != null)
            {
                pessoa.Turmas.ToList()
                        .ForEach(t =>
                        {
                            if (t.IdTurma == 0)
                                contexto.Turmas.Add(t);
                            else
                                contexto.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        });
            }

            if (pessoa.Curso != null)
            {
                pessoa.Curso.ToList()
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
            }

            contexto.Pessoas.Add(pessoa);
        }

        public bool VerificarSeExistePessoaComMesmoCPF(string CPF, int idPessoa)
        {
            return this.contexto.Pessoas.Any(p => p.CPF == CPF && p.IdPessoa != idPessoa);
        }

        public bool VerificarSeExistePessoaComMesmoRNE(string RNE, int idPessoa)
        {
            return this.contexto.Pessoas.Any(p => p.RNE == RNE && p.IdPessoa != idPessoa);
        }

        public List<Pessoa> GetDataFromDatabase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            return base.GetDataFromDb(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
        }

        public override ExpressionStarter<Pessoa> ConfigureFilter(ExpressionStarter<Pessoa> predicate, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                searchValue = searchValue.ToUpper();

                predicate = predicate.Or(e => e.Nome.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.NomeCompleto.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.CPF.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.RG.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.TelefoneEmergencia.ToUpper().Contains(searchValue));
                predicate = predicate.Or(e => e.DataNascimento.ToString().Contains(searchValue));
            }

            return predicate;
        }
    }
}
