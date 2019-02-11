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
    public class Empresa : Base
    {
        public Empresa() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdEmpresa { get; set; }

        [Required]
        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Required]
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        public string Complemento { get; set; }

        [Display(Name = "Número")]
        public int Numero { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        [Required]
        [Display(Name = "CNPJ")]
        public string CGC { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "Tipo de Cobrança")]
        public Nullable<TipoCobranca> TipoCobranca { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public string CEP { get; set; }

        public string NumeroContrato { get; set; }

        public string Email { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Display(Name = "Área de Atuação")]
        public TipoEmpresa TipoEmpresa { get; set; }
        [Column("TipoEmpresa_IdTipoEmpresa")]
        [ForeignKey("TipoEmpresa")]
        public Nullable<int> TipoEmpresaId { get; set; }

        public bool FlgNaoExigeCursoParaAreaRestrita { get; set; }

        public List<Pessoa> Pessoas { get; set; }
        public List<Veiculo> Veiculos { get; set; }
        public List<Solicitacao> Solicitacoes { get; set; }
        public List<Credencial> Credenciais { get; set; }
        public List<Contrato> Contratos { get; set; }
        public List<Apolice> Apolices { get; set; }
        public Aeroporto Aeroporto { get; set; }
        [Column("Aeroporto_IdAeroporto")]
        [ForeignKey("Aeroporto")]
        public Nullable<int> AeroportoId { get; set; }


    }
}
