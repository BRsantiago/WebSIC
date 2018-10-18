using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Veiculo : Base
    {
        public Veiculo() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdVeiculo { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        [Display(Name = "Ano de Fabricação")]
        public string AnoFabricacao { get; set; }
        [Display(Name = "Ano do Modelo")]
        public string AnoModelo { get; set; }
        public string Cor { get; set; }

        [Required]
        public string Placa { get; set; }
        [Required]
        [Display(Name = "Nº do Chassi")]
        public string Chassi { get; set; }

        [Display(Name = "Observações")]
        public string Observacao { get; set; }

        [Display(Name = "Nº da Apólice")]
        public Apolice Apolice { get; set; }

        public Empresa Empresa { get; set; }

        public IList<Solicitacao> Solicitacoes { get; set; }

        public IList<Credencial> Credenciais { get; set; }
    }
}
