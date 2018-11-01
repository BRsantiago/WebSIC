using Entity.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Solicitacao : Base
    {
        public Solicitacao() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdSolicitacao { get; set; }

        public DateTime DataAutorizacao { get; set; }

        [Display(Name = "Documentação")]
        public string CaminhoArquivoDigitalizado { get; set; }
        [Display(Name = "Tipo de Solicitação")]
        public TipoSolicitacao TipoSolicitacao { get; set; }
        [Display(Name = "Tipo de Emissão")]
        public TipoEmissao TipoEmissao { get; set; }
        [Display(Name = "1ª Área")]
        public Area Area1 { get; set; }
        [Display(Name = "2ª Área")]
        public Area Area2 { get; set; }
        [Display(Name = "Portão de Acesso")]
        public PortaoAcesso PortaoAcesso { get; set; }
                
        public Empresa Empresa { get; set; }
        public Contrato Contrato { get; set; }

        public Veiculo Veiculo { get; set; }
        public Pessoa Pessoa { get; set; }

        public Schedule Schedule { get; set; }
        public Cargo Cargo { get; set; }
        public Credencial Credencial { get; set; }
    }
}
