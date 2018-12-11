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
        [Display(Name = "Data de Início da Vigência")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataInicioVigencia { get; set; }

        [Required]
        [Display(Name = "Data de Validade")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataValidade { get; set; }

        [Display(Name = "Tipo de Seguro")]
        public string Seguro { get; set; }

        [Display(Name = "Seguradora Contratada")]
        public string Seguradora { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public Empresa Empresa { get; set; }

        [Column("Empresa_IdEmpresa")]
        [ForeignKey("Empresa")]
        public Nullable<int> EmpresaId { get; set; }

        public IList<Veiculo> Veiculos { get; set; }
    }
}
