using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Credencial : Base
    {
        public Credencial() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Matrícula")]
        public int IdCredencial { get; set; }

        public DateTime? DataDesativacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public DateTime? DataExpedicao { get; set; }

        public bool FlgTemporario { get; set; }

        public Pessoa Pessoa { get; set; }
        public Cargo Cargo { get; set; }
        public string NomeImpressaoFrenteCracha { get; set; }
        public string DescricaoFuncaoFrenteCracha { get; set; }
        public string CategoriaMotorista1 { get; set; }
        public string CategoriaMotorista2 { get; set; }
        public bool FlgCVE { get; set; }

        public Aeroporto Aeroporto { get; set; }
        public Empresa Empresa { get; set; }
        public Contrato Contrato { get; set; }

        public Veiculo Veiculo { get; set; }
        public Area Area1 { get; set; }
        public Area Area2 { get; set; }
        public PortaoAcesso PortaoAcesso { get; set; }
        
        public IList<Solicitacao> Solicitacoes { get; set; }
        public IList<Ocorrencia> Ocorrencias { get; set; }

    }
}
