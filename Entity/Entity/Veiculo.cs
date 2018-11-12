using Entity.Enum;
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
        [Display(Name = "Tipo de Serviço")]
        public TipoServico TipoServico { get; set; }
        [Display(Name = "Categoria do Veículo")]
        public Categoria Categoria { get; set; }
        [Display(Name = "Acesso à Área de Manobra")]
        public bool AcessoManobra { get; set; }

        [Required]
        public string Placa { get; set; }
        [Required]
        [Display(Name = "Nº do Chassi")]
        public string Chassi { get; set; }

        [Display(Name = "Observações")]
        public string Observacao { get; set; }

        [Display(Name = "Nº da Apólice")]
        public Apolice Apolice { get; set; }
        [Column("Apolice_IdApolice")]
        [ForeignKey("Apolice")]
        public Nullable<int> ApoliceId { get; set; }

        public Empresa Empresa { get; set; }
        [Column("Empresa_IdEmpresa")]
        [ForeignKey("Empresa")]
        public Nullable<int> EmpresaId { get; set; }

        public IList<Solicitacao> Solicitacoes { get; set; }

        public IList<Credencial> Credenciais { get; set; }
    }
}
