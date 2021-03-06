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

            if (pessoa.Empresas != null && pessoa.Empresas.Any())
                throw new Exception("Essa pessoa não pode ser excluída pois a mesma é representante de empresa.");
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
            if (pessoa.CPF != null && !isCPF(pessoa.CPF))
                throw new Exception("CPF inválido. Favor verificar !");

            if (pessoa.DataValidadeFoto.HasValue && pessoa.DataValidadeFoto.Value < DateTime.Now)
                throw new Exception("A foto desta pessoa tem mais de dois anos, favor capturar uma nova foto.");

            if ((String.IsNullOrEmpty(pessoa.CPF) || String.IsNullOrWhiteSpace(pessoa.CPF)) && (String.IsNullOrEmpty(pessoa.RNE) || String.IsNullOrWhiteSpace(pessoa.RNE)))
                throw new Exception("Favor informar o CPF ou RNE.");

            if (pessoa.CPF != null && this.PessoaRepository.VerificarSeExistePessoaComMesmoCPF(pessoa.CPF, pessoa.IdPessoa))
                throw new Exception("Já existe uma pessoa cadastrada com este CPF.");

            if (pessoa.RNE != null && this.PessoaRepository.VerificarSeExistePessoaComMesmoRNE(pessoa.RNE, pessoa.IdPessoa))
                throw new Exception("Já existe uma pessoa cadastrada com este RNE.");
        }

        public List<Pessoa> GetDataList(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            try
            {
                pessoas = PessoaRepository.GetDataFromDatabase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            }
            catch (Exception ex)
            {
                filteredResultsCount =
                    totalResultsCount = 0;
            }

            return pessoas;
        }

        public bool isCPF(string valor)
        {
            if (valor == "")
                return false;

            valor = valor.Replace(".", string.Empty);
            valor = valor.Replace("-", string.Empty);

            string cpf = valor;

            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "2222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            try
            {
                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1));
                n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1));
                n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1));
                n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1));
                n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1));
                n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
                soma += (peso1[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
                soma += (peso2[i] * Convert.ToInt32(n[i]));

            // Pega o resto da divisao
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao é válido
            if (calculado == digitado)
                return (true);
            else
                return (false);
        }
    }
}
