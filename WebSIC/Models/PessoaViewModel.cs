﻿using Entity.Enum;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class PessoaViewModel
    {
        public int IdPessoa { get; set; }

        [Required(ErrorMessage = "Favor informar o nome completo.")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Data de Nasc.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public string DataNascimento { get; set; }

        [Display(Name = "Pai")]
        public string NomePai { get; set; }

        [Display(Name = "Mãe")]
        public string NomeMae { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }

        [Required(ErrorMessage = "Favor informar o tel. de emergência.")]
        [Display(Name = "Emergência")]
        public string TelefoneEmergencia { get; set; }

        [Display(Name = "Residencial")]
        public string TelefoneResidencial { get; set; }

        [Display(Name = "Celular")]
        public string TelefoneCelular { get; set; }

        public string RNE { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

        [Display(Name = "Orgão Exped.")]
        public string OrgaoExpeditor { get; set; }

        [Display(Name = "UF")]
        public string UFOrgaoExpeditor { get; set; }

        public string IdGenero { get; set; }
        [Display(Name = "Gênero")]
        public Genero Genero { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "CVE")]
        public bool FlgCVE { get; set; }

        public string Email { get; set; }
        public string CNH { get; set; }

        public string IdCategoriaUm { get; set; }
        [Display(Name = "Categoria")]
        public Categoria CategoriaUm { get; set; }

        public string IdCategoriaDois { get; set; }
        [Display(Name = "Categoria")]
        public Categoria CategoriaDois { get; set; }

        [Display(Name = "Validade")]
        public string DataValidadeCNH { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Foto { get; set; }

        public string ImageUrl { get; set; }
        public string DataValidadeFoto { get; set; }

        public Usuario Usuario { get; set; }
        public IList<Solicitacao> Solicitacaos { get; set; }
        public IList<Turma> Turmas { get; set; }
        public IList<Credencial> Credenciais { get; set; }
        public IList<Empresa> Empresas { get; set; }
        public IList<CursoSemTurma> Curso { get; set; }

        [Display(Name = "Data de Criação")]
        public DateTime Criacao { get; set; }

        [Display(Name = "Criado Por")]
        public string Criador { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTime Atualizacao { get; set; }

        [Display(Name = "Atualizado Por")]
        public string Atualizador { get; set; }

        [Display(Name = "Ativo/Inativo")]
        public bool Ativo { get; set; }

        [Display(Name = "Residência fora do país nos ultimos 10 anos")]
        public bool FlgResidenciaForaDoPaisNosUltimos10Anos { get; set; }

        [Display(Name = "Observação")]
        public string ObservacaoResidenciaForaDoPaisNosUltimos10Anos { get; set; }

        [Display(Name = "Número do Colete")]
        public string NumeroColete { get; set; }

        [Display(Name = "Anexo - RG")]
        public string RGFilePath { get; set; }
        public HttpPostedFileBase RGFile { get; set; }

        [Display(Name = "Anexo - CR")]
        public string CRFilePath { get; set; }
        public HttpPostedFileBase CRFile { get; set; }

        [Display(Name = "Anexo - CNH")]
        public string CNHFilePath { get; set; }
        public HttpPostedFileBase CNHFile { get; set; }

        [Display(Name = "Anexo - CTPS")]
        public string CTPSFilePath { get; set; }
        public HttpPostedFileBase CTPSFile { get; set; }

        public bool TemCredencialVencida { get; set; }

        public PessoaViewModel() { }

        public PessoaViewModel(Pessoa pessoa)
        {
            this.IdPessoa = pessoa.IdPessoa;
            this.NomeCompleto = pessoa.NomeCompleto;
            this.Nome = pessoa.Nome;
            this.DataNascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy");
            this.NomePai = pessoa.NomePai;
            this.NomeMae = pessoa.NomeMae;
            this.Endereco = pessoa.Endereco;
            this.Numero = pessoa.Numero.ToString();
            this.Complemento = pessoa.Complemento;
            this.Bairro = pessoa.Bairro;
            this.Cidade = pessoa.Cidade;
            this.UF = pessoa.UF;
            this.CEP = pessoa.CEP;
            this.TelefoneEmergencia = pessoa.TelefoneEmergencia;
            this.TelefoneResidencial = pessoa.TelefoneResidencial;
            this.TelefoneCelular = pessoa.TelefoneCelular;
            this.RNE = pessoa.RNE;
            this.CPF = pessoa.CPF;
            this.RG = pessoa.RG;
            this.OrgaoExpeditor = pessoa.OrgaoExpeditor;
            this.UFOrgaoExpeditor = pessoa.UFOrgaoExpeditor;
            this.Genero = pessoa.Genero;
            this.Observacao = pessoa.Observacao;
            this.FlgCVE = pessoa.FlgCVE;
            this.Email = pessoa.Email;
            this.CNH = pessoa.CNH;
            this.CategoriaUm = pessoa.CategoriaUm;
            this.CategoriaDois = pessoa.CategoriaDois;
            this.DataValidadeCNH = pessoa.DataValidadeCNH.ToString();
            this.ImageUrl = pessoa.ImageUrl;
            this.Usuario = pessoa.Usuario;
            this.Solicitacaos = pessoa.Solicitacaos;
            this.Turmas = pessoa.Turmas;
            this.Credenciais = pessoa.Credenciais;
            this.Empresas = pessoa.Empresas;
            this.Curso = pessoa.Curso;
            this.Criacao = pessoa.Criacao;
            this.Criador = pessoa.Criador;
            this.Atualizacao = pessoa.Atualizacao;
            this.Atualizador = pessoa.Atualizador;
            this.Ativo = pessoa.Ativo;
            this.DataValidadeFoto = pessoa.DataValidadeFoto.ToString();
            this.FlgResidenciaForaDoPaisNosUltimos10Anos = pessoa.FlgResidenciaForaDoPaisNosUltimos10Anos;
            this.ObservacaoResidenciaForaDoPaisNosUltimos10Anos = pessoa.ObservacaoResidenciaForaDoPaisNosUltimos10Anos;
            this.NumeroColete = pessoa.NumeroColete;
            this.TemCredencialVencida = (pessoa.Credenciais != null && pessoa.Credenciais.Any(c => c.DataDesativacao == null && c.DataVencimento < DateTime.Now));

            this.RGFilePath = pessoa.RGFilePath;
            this.CRFilePath = pessoa.CRFilePath;
            this.CNHFilePath = pessoa.CNHFilePath;
            this.CTPSFilePath = pessoa.CTPSFilePath;
        }

        public Pessoa MapearParaObjetoDominio()
        {
            Pessoa pessoa = new Pessoa();
            
            pessoa.IdPessoa = this.IdPessoa;
            pessoa.NomeCompleto = this.NomeCompleto.ToUpper();
            pessoa.Nome = String.IsNullOrEmpty(this.Nome) ? this.Nome : this.Nome.ToUpper();
            pessoa.DataNascimento = Convert.ToDateTime(this.DataNascimento);
            pessoa.NomePai = String.IsNullOrEmpty(this.NomePai) ? this.NomePai : this.NomePai.ToUpper();
            pessoa.NomeMae = String.IsNullOrEmpty(this.NomeMae) ? this.NomeMae : this.NomeMae.ToUpper();
            pessoa.Endereco = this.Endereco;
            pessoa.Numero = Convert.ToInt32(this.Numero);
            pessoa.Complemento = String.IsNullOrEmpty(this.Complemento) ? this.Complemento : this.Complemento.ToUpper();
            pessoa.Bairro = String.IsNullOrEmpty(this.Bairro) ? this.Bairro : this.Bairro.ToUpper();
            pessoa.Cidade = String.IsNullOrEmpty(this.Cidade) ? this.Cidade : this.Cidade.ToUpper();
            pessoa.UF = String.IsNullOrEmpty(this.UF) ? this.UF : this.UF.ToUpper();
            pessoa.CEP = this.CEP;
            pessoa.TelefoneEmergencia = this.TelefoneEmergencia;
            pessoa.TelefoneResidencial = this.TelefoneResidencial;
            pessoa.TelefoneCelular = this.TelefoneCelular;
            pessoa.RNE = this.RNE;
            pessoa.CPF = this.CPF;
            pessoa.RG = this.RG;
            pessoa.OrgaoExpeditor = String.IsNullOrEmpty(this.OrgaoExpeditor) ? this.OrgaoExpeditor : this.OrgaoExpeditor.ToUpper();
            pessoa.UFOrgaoExpeditor = String.IsNullOrEmpty(this.UFOrgaoExpeditor) ? this.UFOrgaoExpeditor : this.UFOrgaoExpeditor.ToUpper();
            pessoa.Genero = this.Genero;
            pessoa.Observacao = this.Observacao;
            pessoa.FlgCVE = this.FlgCVE;
            pessoa.Email = this.Email;
            pessoa.CNH = this.CNH;
            pessoa.CategoriaUm = this.CategoriaUm;
            pessoa.CategoriaDois = this.CategoriaDois;
            if (!String.IsNullOrEmpty(this.DataValidadeCNH)) pessoa.DataValidadeCNH = Convert.ToDateTime(this.DataValidadeCNH);
            pessoa.Usuario = this.Usuario;
            pessoa.Solicitacaos = this.Solicitacaos;
            pessoa.Turmas = this.Turmas;
            pessoa.Credenciais = this.Credenciais;
            pessoa.Empresas = this.Empresas;
            pessoa.Curso = this.Curso;
            pessoa.Atualizacao = DateTime.Now;
            pessoa.Atualizador = "";
            pessoa.Ativo = this.Ativo;
            pessoa.ImageUrl = this.ImageUrl;
            if (!String.IsNullOrEmpty(this.DataValidadeFoto)) pessoa.DataValidadeFoto = Convert.ToDateTime(this.DataValidadeFoto);
            pessoa.FlgResidenciaForaDoPaisNosUltimos10Anos = this.FlgResidenciaForaDoPaisNosUltimos10Anos;
            pessoa.ObservacaoResidenciaForaDoPaisNosUltimos10Anos = this.ObservacaoResidenciaForaDoPaisNosUltimos10Anos;
            pessoa.NumeroColete = this.NumeroColete;

            pessoa.RGFilePath = this.RGFilePath;
            pessoa.CRFilePath = this.CRFilePath;
            pessoa.CNHFilePath = this.CNHFilePath;
            pessoa.CTPSFilePath = this.CTPSFilePath;

            return pessoa;
        }

    }
}
