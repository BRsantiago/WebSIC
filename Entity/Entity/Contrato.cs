using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Contrato : Base
    {
        public Contrato() : base() { }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdContrato { get; set; }
        public string Numero { get; set; }
        [Required]
        public DateTime InicioVigencia { get; set; }
        [Required]
        public DateTime FimVigencia { get; set; }

        public Empresa Empresa { get; set; }
        public IList<Solicitacao> Solicitacoes { get; set; }
    }
}
