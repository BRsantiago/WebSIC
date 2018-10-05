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

        public bool FlgMotorista { get; set; }
        public bool FlgTemporario { get; set; }
        
        public DateTime DataAutorizacao { get; set; }

        public TipoSolicitacao TipoSolicitacao { get; set; }
        public Pessoa Pessoa { get; set; }
        public Empresa Empresa { get; set; }
        public Veiculo Veiculo { get; set; }
        public Schedule Schedule { get; set; }
        public Contrato Contrato { get; set; }
        public Credencial Credencial { get; set; }
        public Area Area1 { get; set; }
        public Area Area2 { get; set; }
        public PortaoAcesso PortaoAcesso { get; set; }

    }
}
