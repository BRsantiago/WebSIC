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
        [Column("TipoSolicitacao_IdTipoSolicitacao")]
        [ForeignKey("TipoSolicitacao")]
        public Nullable<int> TipoSolicitacaoId { get; set; }

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

        [Display(Name = "Aeroporto")]
        public Aeroporto Aeroporto { get; set; }

        [Column("Aeroporto_IdAeroporto")]
        [ForeignKey("Aeroporto")]
        public Nullable<int> AeroportoId { get; set; }

        [Display(Name = "Ramo de Atividade")]
        public RamoAtividade RamoAtividade { get; set; }

        [Column("RamoAtividade_IdRamoAtividade")]
        [ForeignKey("RamoAtividade")]
        public Nullable<int> RamoAtividadeId { get; set; }

        [Display(Name = "Certidão Negativa TRF")]
        public string CertTRFFilePath { get; set; }

        [Display(Name = "Certidão Antecedentes Criminais PF")]
        public string CertAntCrimPFFilePath { get; set; }

        [Display(Name = "Certidão Antecedentes Criminais PC")]
        public string CertAntCrimPCFilePath { get; set; }

        [Display(Name = "Certidão Negativa TJBA")]
        public string CertTJBAFilePath { get; set; }
    }
}
