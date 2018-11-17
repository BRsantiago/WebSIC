﻿using Entity.Entities;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class PessoaService : IPessoaService
    {
        public IPessoaRepository PessoaRepository;
        public ICursoRepository CursoRepository;
        public ICursoSemTurmaRepository CursoSemTurmaRepository;

        public PessoaService(IPessoaRepository _PessoaRepository,
                                ICursoRepository _CursoRepository,
                                ICursoSemTurmaRepository _CursoSemTurmaRepository)
        {
            PessoaRepository = _PessoaRepository;
            CursoRepository = _CursoRepository;
            CursoSemTurmaRepository = _CursoSemTurmaRepository;
        }

        public Pessoa ObterPorCPF(string cpf)
        {
            return this.PessoaRepository.ObterPorCPF(cpf);
        }

        public List<Pessoa> ObterPorEmpresa(int idEmpresa)
        {
            return this.PessoaRepository.ObterPorEmpresa(idEmpresa);
        }

        public void IncluirPessoa(Pessoa pessoa)
        {
            this.Validar(pessoa);
            this.IncluirCursoDDA(pessoa);
            PessoaRepository.IncluirNovaPessoa(pessoa);
            PessoaRepository.Salvar();
        }

        public Pessoa IncluirNovoRepresentante(Pessoa representante)
        {
            try
            {
                PessoaRepository.IncluirNovoRepresentante(representante);
                PessoaRepository.Salvar();

                return representante;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public Pessoa ObterPorId(string id)
        {
            return this.PessoaRepository.ObterPorId(Convert.ToInt32(id));
        }

        public List<Pessoa> ObterTodos()
        {
            return this.PessoaRepository.ObterTodos();
        }

        public void Atualizar(Pessoa pessoa)
        {
            this.Validar(pessoa);
            IncluirCursoDDA(pessoa);
            PessoaRepository.AtualizarRepresentante(pessoa);
            PessoaRepository.Salvar();
        }

        public void ExcluirRepresentante(Pessoa representante, int idEmpresa)
        {
            representante.Empresas.Remove(representante.Empresas.Where(e => e.IdEmpresa == idEmpresa).SingleOrDefault());
            this.Atualizar(representante);
        }

        public void ExcluirPessoa(Pessoa pessoa)
        {
            this.ValidarParaExcluir(pessoa);

            PessoaRepository.Remover(pessoa);
            PessoaRepository.Salvar();
        }

        private void ValidarParaExcluir(Pessoa pessoa)
        {
            if (pessoa.Credenciais != null && pessoa.Credenciais.Any(c => c.DataExpedicao.HasValue))
                throw new Exception("Essa pessoa não pode ser excluída pois já existe credencial emitida.");

            if (pessoa.Solicitacaos != null && pessoa.Solicitacaos.Any())
                throw new Exception("Essa pessoa não pode ser excluída pois já existe solicitação de credencial.");
        }

        private void IncluirCursoDDA(Pessoa pessoa)
        {
            List<Curso> cursosRealizados = this.CursoRepository.ObterCursosRealizadosComValidadePorIdPessoa(pessoa.IdPessoa);

            if (cursosRealizados == null || (cursosRealizados != null && !cursosRealizados.Any(c => c.PermiteDirigirEmAreasRestritas)) && !String.IsNullOrEmpty(pessoa.CNH))
            {
                if (cursosRealizados == null)
                    pessoa.Curso = new List<CursoSemTurma>();

                Curso DDA = CursoRepository.ObterTodos().Where(x => x.PermiteDirigirEmAreasRestritas).SingleOrDefault();

                pessoa.Curso.Add(new CursoSemTurma()
                {
                    Curso = DDA,
                    CursoId = DDA.IdCurso,
                    DataValidade = DateTime.Now.AddDays(DDA.Validade),
                    PessoaId = pessoa.IdPessoa,
                    Criacao = DateTime.Now,
                    Atualizacao = DateTime.Now,
                });
            }
        }

        private void Validar(Pessoa pessoa)
        {
            if (pessoa.DataValidadeFoto.HasValue && pessoa.DataValidadeFoto.Value < DateTime.Now)
                throw new Exception("A foto desta pessoa tem mais de dois anos, favor capturar uma nova foto.");

            if (String.IsNullOrEmpty(pessoa.CPF) || String.IsNullOrWhiteSpace(pessoa.CPF))
                throw new Exception("Favor informar o cpf.");

            Pessoa pessoaBase = this.PessoaRepository.ObterPorCPF(pessoa.CPF);
            if (pessoa.CPF == pessoaBase.CPF && pessoa.IdPessoa != pessoaBase.IdPessoa)
                throw new Exception("Já existe uma pessoa cadastrada com este CPF.");
        }

    }
}
