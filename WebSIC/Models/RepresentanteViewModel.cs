using Entity.DTO;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class RepresentanteViewModel
    {
        public string IdPessoa { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Apelido { get; set; }
        [Display(Name = "Data de Nascimento")]
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
        [Required]
        [Display(Name = "Tel. Emergência")]
        public string TelefoneEmergencia { get; set; }
        [Display(Name = "Tel. Residencial")]
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
        [Display(Name = "Gênero")]
        public Genero Genero { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public string Email { get; set; }
        public string CNH { get; set; }
        public string CategoriaCNH { get; set; }
        public string DataValidadeCNH { get; set; }

        public string IdEmpresa { get; set; }

        public RepresentanteViewModel() { }

        public RepresentanteViewModel(Pessoa representante)
        {
            IdPessoa = representante.IdPessoa.ToString();
            Nome = representante.Nome;
            Apelido = representante.Apelido;
            DataNascimento = representante.DataNascimento;
            NomePai = representante.NomePai;
            NomeMae = representante.NomeMae;
            Endereco = representante.Endereco;
            Numero = representante.Numero;
            Complemento = representante.Complemento;
            Bairro = representante.Bairro;
            Cidade = representante.Cidade;
            UF = representante.UF;
            CEP = representante.CEP;
            TelefoneEmergencia = representante.TelefoneEmergencia;
            TelefoneResidencial = representante.TelefoneResidencial;
            TelefoneCelular = representante.TelefoneCelular;
            RNE = representante.RNE;
            CPF = representante.CPF;
            RG = representante.RG;
            OrgaoExpeditor = representante.OrgaoExpeditor;
            UFOrgaoExpeditor = representante.UFOrgaoExpeditor;
            Genero = representante.Genero;
            Observacao = representante.Observacao;
            Email = representante.Email;
            CNH = representante.CNH;
            DataValidadeCNH = representante.DataValidadeCNH;
        }

        public Pessoa MapearParaObjetoDominio()
        {
            Pessoa pessoa = new Pessoa();

            pessoa.IdPessoa = Convert.ToInt32(this.IdPessoa);
            pessoa.Nome = this.Nome;
            pessoa.Apelido = this.Apelido;
            pessoa.DataNascimento = this.DataNascimento;
            pessoa.NomePai = this.NomePai;
            pessoa.NomeMae = this.NomeMae;
            pessoa.Endereco = this.Endereco;
            pessoa.Numero = this.Numero;
            pessoa.Complemento = this.Complemento;
            pessoa.Bairro = this.Bairro;
            pessoa.Cidade = this.Cidade;
            pessoa.UF = this.UF;
            pessoa.CEP = this.CEP;
            pessoa.TelefoneEmergencia = this.TelefoneEmergencia;
            pessoa.TelefoneResidencial = this.TelefoneResidencial;
            pessoa.TelefoneCelular = this.TelefoneCelular;
            pessoa.RNE = this.RNE;
            pessoa.CPF = this.CPF;
            pessoa.RG = this.RG;
            pessoa.OrgaoExpeditor = this.OrgaoExpeditor;
            pessoa.UFOrgaoExpeditor = this.UFOrgaoExpeditor;
            pessoa.Genero = this.Genero;
            pessoa.Observacao = this.Observacao;
            pessoa.Email = this.Email;
            pessoa.CNH = this.CNH;
            pessoa.DataValidadeCNH = this.DataValidadeCNH;


            pessoa.Empresas = new List<Empresa>();
            pessoa.Empresas.Add(new Empresa() { IdEmpresa = Convert.ToInt32(this.IdEmpresa) });

            return pessoa;
        }
    }
}