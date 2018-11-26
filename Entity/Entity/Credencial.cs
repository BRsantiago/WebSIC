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

        [Display(Name = "Data de Desativação")]
        public DateTime? DataDesativacao { get; set; }
        [Display(Name = "Data de Vencimento")]
        public DateTime? DataVencimento { get; set; }
        [Display(Name = "Data de Expedição")]
        public DateTime? DataExpedicao { get; set; }

        [Display(Name = "Nome")]
        public string NomeImpressaoFrenteCracha { get; set; }
        [Display(Name = "Função")]
        public string DescricaoFuncaoFrenteCracha { get; set; }
        public string CategoriaMotorista1 { get; set; }
        public string CategoriaMotorista2 { get; set; }
        public bool FlgCVE { get; set; }
        public bool FlgTemporario { get; set; }
        public bool FlgSegundaVia { get; set; }

        public Pessoa Pessoa { get; set; }
        [Column("Pessoa_IdPessoa")]
        [ForeignKey("Pessoa")]
        public Nullable<int> PessoaId { get; set; }

        public Cargo Cargo { get; set; }
        [Column("Cargo_IdCargo")]
        [ForeignKey("Cargo")]
        public Nullable<int> CargoId { get; set; }

        public Aeroporto Aeroporto { get; set; }
        [Column("Aeroporto_IdAeroporto")]
        [ForeignKey("Aeroporto")]
        public Nullable<int> AeroportoId { get; set; }

        public Empresa Empresa { get; set; }
        [Column("Empresa_IdEmpresa")]
        [ForeignKey("Empresa")]
        public Nullable<int> EmpresaId { get; set; }

        public Contrato Contrato { get; set; }
        [Column("Contrato_IdContrato")]
        [ForeignKey("Contrato")]
        public Nullable<int> ContratoId { get; set; }

        public Veiculo Veiculo { get; set; }
        [Column("Veiculo_IdVeiculo")]
        [ForeignKey("Veiculo")]
        public Nullable<int> VeiculoId { get; set; }

        [Display(Name = "1ª Área")]
        public Area Area1 { get; set; }
        [Column("Area1_IdArea")]
        [ForeignKey("Area1")]
        public Nullable<int> Area1Id { get; set; }

        [Display(Name = "2ª Área")]
        public Area Area2 { get; set; }
        [Column("Area2_IdArea")]
        [ForeignKey("Area2")]
        public Nullable<int> Area2Id { get; set; }

        [Display(Name = "1º  Portão de Acesso")]
        public PortaoAcesso PortaoAcesso1 { get; set; }

        [Column("PortaoAcesso1_IdPortaoAcesso")]
        [ForeignKey("PortaoAcesso1")]
        public Nullable<int> PortaoAcesso1Id { get; set; }

        [Display(Name = "2º  Portão de Acesso")]
        public PortaoAcesso PortaoAcesso2 { get; set; }

        [Column("PortaoAcesso2_IdPortaoAcesso")]
        [ForeignKey("PortaoAcesso2")]
        public Nullable<int> PortaoAcesso2Id { get; set; }

        [Display(Name = "3º  Portão de Acesso")]
        public PortaoAcesso PortaoAcesso3 { get; set; }

        [Column("PortaoAcesso3_IdPortaoAcesso")]
        [ForeignKey("PortaoAcesso3")]
        public Nullable<int> PortaoAcesso3Id { get; set; }

        public IList<Solicitacao> Solicitacoes { get; set; }
        public IList<Ocorrencia> Ocorrencias { get; set; }

    }
}
