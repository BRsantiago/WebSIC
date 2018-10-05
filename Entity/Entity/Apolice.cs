using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Apolice : Base
    {
        public Apolice() : base() { }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdApolice { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public DateTime DataValidade { get; set; }

        public string Observacao { get; set; }

        public Empresa Empresa { get; set; }
        public IList<Veiculo> Veiculos { get; set; }
    }
}
