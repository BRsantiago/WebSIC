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
    public class Solicitacao : Base
    {
        public Solicitacao() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdSolicitacao { get; set; }

        [Display(Name = "Data de Aprovação")]
        public Nullable<DateTime> DataAutorizacao { get; set; }

        [Display(Name = "Documentação")]
        public string CaminhoArquivoDigitalizado { get; set; }

        [Display(Name = "Tipo de Solicitação")]
        public TipoSolicitacao TipoSolicitacao { get; set; }

        [Display(Name = "Tipo de Emissão")]
        public TipoEmissao TipoEmissao { get; set; }

        public Schedule Schedule { get; set; }
        
        public Credencial Credencial { get; set; }
        [Column("Credencial_IdCredencial")]
        [ForeignKey("Credencial")]
        public Nullable<int> CredencialId { get; set; }

        public Pessoa Pessoa { get; set; }

        [Column("Pessoa_IdPessoa")]
        [ForeignKey("Pessoa")]
        public Nullable<int> PessoaId { get; set; }

        public Cargo Cargo { get; set; }

        [Column("Cargo_IdCargo")]
        [ForeignKey("Cargo")]
        public Nullable<int> CargoId { get; set; }

        public Empresa Empresa { get; set; }

        [Column("Empresa_IdEmpresa")]
        [ForeignKey("Empresa")]
        public Nullable<int> EmpresaId { get; set; }

        public Contrato Contrato { get; set; }

        [Column("Contrato_IdContrato")]
        [ForeignKey("Contrato")]
        public Nullable<int> ContratoId { get; set; }

        [Display(Name = "Veículo")]
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

        [Display(Name = "Portão de Acesso")]
        public PortaoAcesso PortaoAcesso { get; set; }

        [Column("PortaoAcesso_IdPortaoAcesso")]
        [ForeignKey("PortaoAcesso")]
        public Nullable<int> PortaoAcessoId { get; set; }

        [Display(Name = "Aeroporto")]
        public Aeroporto Aeroporto { get; set; }

        [Column("Aeroporto_IdAeroporto")]
        [ForeignKey("Aeroporto")]
        public Nullable<int> AeroportoId { get; set; }

        [Display(Name = "Ramo de Atividade")]
        public RamoAtividade RamoAtividade { get; set; }
    }
}
