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

        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdPessoa { get; set; }
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
        public string Genero { get; set; }
        [Display(Name = "Observação")]
        public string Observacao { get; set; }
        public bool FlgCVE { get; set; }
        public string Email { get; set; }
        public string CNH { get; set; }
        public string CategoriaCNH { get; set; }
        public string DataValidadeCNH { get; set; }

        public Usuario Usuario { get; set; }
        public IList<Solicitacao> Solicitacaos { get; set; }
        public IList<Turma> Turmas { get; set; }
        public IList<Credencial> Credenciais { get; set; }
        public IList<Empresa> Empresas { get; set; }
    }
}
