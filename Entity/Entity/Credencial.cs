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
        public string IdCredencial { get; set; }
        [Required]
        public string Matricula { get; set; }
        public bool FlgMotorista { get; set; }
        public bool FlgTemporario { get; set; }
        public bool FlgCVE { get; set; }

        [Required]
        public string DataExpedicao { get; set; }
        public string DataDesativacao { get; set; }
        [Required]
        public string DataVencimento { get; set; }



        public Pessoa Pessoa { get; set; }
        public Veiculo Veiculo { get; set; }
        public Empresa Empresa { get; set; }
        public Aeroporto Aeroporto { get; set; }
        public Area Area1 { get; set; }
        public Area Area2 { get; set; }
        public Cargo Cargo { get; set; }
        public IList<Solicitacao> Solicitacoes { get; set; }
        public IList<Ocorrencia> Ocorrencias { get; set; }

    }
}
