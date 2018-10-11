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
        [Display(Name = "Número da Apólice")]
        public string Numero { get; set; }

        [Required]
        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public Empresa Empresa { get; set; }

        public IList<Veiculo> Veiculos { get; set; }
    }
}
