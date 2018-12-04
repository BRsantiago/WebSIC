using Entity.Enum;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class SolicitacaoViewModel
    {
        public SolicitacaoViewModel()
        {
        }

        public int IdAeroporto { get; set; }
        [Display(Name = "Aeroporto")]
        public List<Aeroporto> Aeroportos { get; set; }

        public int IdEmpresa { get; set; }
        [Display(Name = "Empresa")]
        public List<Empresa> Empresas { get; set; }

        public int IdContrato { get; set; }
        [Display(Name = "Contrato")]
        public List<Contrato> Contratos { get; set; }

        public bool FlgTemporario { get; set; }

        public int IdTipoSolicitacao { get; set; }
        [Display(Name = "Tipo de Solicitação")]
        public List<TipoSolicitacao> TiposSolicitacao { get; set; }

        public int IdTipoEmissao { get; set; }
        [Display(Name = "Tipo de Emissão")]
        public TipoEmissao TiposEmissao { get; set; }

        public int IdCargo { get; set; }
        [Display(Name = "Cargo")]
        public List<Cargo> Cargo { get; set; }

        [Display(Name = "Área")]
        public List<Area> Areas { get; set; }
        public int? IdArea1 { get; set; }
        public int? IdArea2 { get; set; }

        public int IdPessoa { get; set; }
        public int IdSolicitacao { get; set; }

        [Display(Name = "Ramo de Atividade")]
        public List<RamoAtividade> RamoAtividade { get; set; }
        public int IdRamoAtividade { get; set; }

        [Display(Name = "Certidão Negativa TRF")]
        public string CertTRFFilePath { get; set; }
        public HttpPostedFileBase CertTRFFile { get; set; }

        [Display(Name = "Certidão Antecedentes Criminais PF")]
        public string CertAntCrimPFFilePath { get; set; }
        public HttpPostedFileBase CertAntCrimPFFile { get; set; }

        [Display(Name = "Certidão Antecedentes Criminais PC")]
        public string CertAntCrimPCFilePath { get; set; }
        public HttpPostedFileBase CertAntCrimPCFile { get; set; }

        [Display(Name = "Certidão Negativa TJBA")]
        public string CertTJBAFilePath { get; set; }
        public HttpPostedFileBase CertTJBAFile { get; set; }


        public SolicitacaoViewModel(Solicitacao solicitacao)
        {
            this.IdSolicitacao = solicitacao.IdSolicitacao;
            this.IdAeroporto = solicitacao.AeroportoId.Value;
            this.IdEmpresa = solicitacao.Empresa.IdEmpresa;
            this.IdContrato = solicitacao.Contrato.IdContrato;
            this.IdTipoSolicitacao = solicitacao.TipoSolicitacao.IdTipoSolicitacao;
            //this.IdTipoEmissao = solicitacao.TipoEmissao;
            this.IdArea1 = solicitacao.Area1 != null ? solicitacao.Area1.IdArea : 0;
            this.IdArea2 = solicitacao.Area2 != null ? solicitacao.Area2.IdArea : 0;
            this.IdPessoa = solicitacao.Pessoa.IdPessoa;
            this.IdCargo = solicitacao.Cargo.IdCargo;
            this.IdRamoAtividade = solicitacao.RamoAtividadeId.Value;

            this.CertAntCrimPCFilePath = solicitacao.CertAntCrimPCFilePath;
            this.CertAntCrimPFFilePath = solicitacao.CertAntCrimPFFilePath;
            this.CertTJBAFilePath = solicitacao.CertTJBAFilePath;
            this.CertTRFFilePath = solicitacao.CertTRFFilePath;
        }

        public Solicitacao MapearParaObjetoDominio()
        {
            Solicitacao solicitacao = new Solicitacao();

            solicitacao.IdSolicitacao = this.IdSolicitacao;
            solicitacao.AeroportoId = this.IdAeroporto;
            solicitacao.EmpresaId = this.IdEmpresa;
            solicitacao.ContratoId = this.IdContrato;
            solicitacao.TipoSolicitacaoId = this.IdTipoSolicitacao;
            solicitacao.Area1Id = this.IdArea1;
            solicitacao.Area2Id = this.IdArea2;
            solicitacao.PessoaId = this.IdPessoa;
            solicitacao.CargoId = this.IdCargo;
            solicitacao.RamoAtividadeId = this.IdRamoAtividade;
            solicitacao.TipoEmissao = this.TiposEmissao;

            solicitacao.CertAntCrimPCFilePath = this.CertAntCrimPCFilePath;
            solicitacao.CertAntCrimPFFilePath = this.CertAntCrimPFFilePath;
            solicitacao.CertTJBAFilePath = this.CertTJBAFilePath;
            solicitacao.CertTRFFilePath = this.CertTRFFilePath;

            return solicitacao;
        }
    }
}