using Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Pessoa : Base
    {
        public Pessoa() : base()
        {
            Curso = new List<CursoSemTurma>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPessoa { get; set; }

        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Data de Nasc.")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Pai")]
        public string NomePai { get; set; }

        [Display(Name = "Mãe")]
        public string NomeMae { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public int? Numero { get; set; }

        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }

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
        public Genero Genero { get; set; }

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
        public DateTime? DataValidadeCNH { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public DateTime? DataValidadeFoto { get; set; }

        [Display(Name = "Residência fora do país nos ultimos 10 anos")]
        public bool FlgResidenciaForaDoPaisNosUltimos10Anos { get; set; }

        [Display(Name = "Observação")]
        public string ObservacaoResidenciaForaDoPaisNosUltimos10Anos { get; set; }

        public Usuario Usuario { get; set; }
        public IList<Solicitacao> Solicitacaos { get; set; }
        public IList<Turma> Turmas { get; set; }
        public IList<Credencial> Credenciais { get; set; }
        public IList<Empresa> Empresas { get; set; }
        public IList<CursoSemTurma> Curso { get; set; }
    }
}
