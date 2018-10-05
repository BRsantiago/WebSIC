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
        public string IdPessoa { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public string DataNascimento { get; set; }
        public string NomePai { get; set; }
        public string NomeMae { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        [Required]
        public string TelefoneEmergencia { get; set; }
        public string TelefoneResidencial { get; set; }
        public string TelefoneCelular { get; set; }
        public string RNE { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string OrgaoExpeditor { get; set; }
        public string UFOrgaoExpeditor { get; set; }
        public string Genero { get; set; }
        public string Observacao { get; set; }
        public string FlgCVE { get; set; }
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
