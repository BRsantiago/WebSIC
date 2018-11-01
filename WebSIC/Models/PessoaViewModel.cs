using Entity.DTO;
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

        [Required]
        public string Nome { get; set; }

        public string Apelido { get; set; }

        [Display(Name = "Data de Nasc.")]
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

        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "CVE")]
        public bool FlgCVE { get; set; }

        public string Email { get; set; }
        public string CNH { get; set; }

        [Display(Name = "Categoria")]
        public Categoria CategoriaUm { get; set; }

        [Display(Name = "Categoria")]
        public Categoria CategoriaDois { get; set; }

        [Display(Name = "Validade")]
        public string DataValidadeCNH { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase Foto { get; set; }

        public string ImageUrl { get; set; }

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

        public PessoaViewModel() { }

        public PessoaViewModel(Pessoa pessoa)
        {
            this.IdPessoa = pessoa.IdPessoa;
            this.Nome = pessoa.Nome;
            this.Apelido = pessoa.Apelido;
            this.DataNascimento = pessoa.DataNascimento;
            this.NomePai = pessoa.NomePai;
            this.NomeMae = pessoa.NomeMae;
            this.Endereco = pessoa.Endereco;
            this.Numero = pessoa.Numero;
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
            this.DataValidadeCNH = pessoa.DataValidadeCNH;
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
        }

        public Pessoa MapearParaObjetoDominio()
        {
            Pessoa pessoa = new Pessoa();

            pessoa.IdPessoa = this.IdPessoa;
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
            pessoa.FlgCVE = this.FlgCVE;
            pessoa.Email = this.Email;
            pessoa.CNH = this.CNH;
            pessoa.CategoriaUm = this.CategoriaUm;
            pessoa.CategoriaDois = this.CategoriaDois;
            pessoa.DataValidadeCNH = this.DataValidadeCNH;
            pessoa.Usuario = this.Usuario;
            pessoa.Solicitacaos = this.Solicitacaos;
            pessoa.Turmas = this.Turmas;
            pessoa.Credenciais = this.Credenciais;
            pessoa.Empresas = this.Empresas;
            pessoa.Curso = this.Curso;
            pessoa.Atualizacao = DateTime.Now;
            pessoa.Atualizador = "";
            pessoa.Ativo = this.Ativo;

            if (this.Foto != null && this.Foto.ContentLength > 0)
            {
                var uploadDir = "/Images/Logo";
                var imagePath = HttpContext.Current.Server.MapPath(uploadDir) + "/" + this.Foto.FileName;//Path.Combine(Server.MapPath(uploadDir), model.Logotipo.FileName);
                var imageUrl = Path.Combine(uploadDir, this.Foto.FileName);
                this.Foto.SaveAs(imagePath);

                pessoa.ImageUrl = imageUrl;
            }

            return pessoa;
        }
    }
}
