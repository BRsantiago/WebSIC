using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class SolicitacaoViewModel
    {
        public int IdAeroporto { get; set; }
        [Display(Name = "Aeroporto")]
        public List<Aeroporto> Aeroportos { get; set; }

        public int IdEmpresa { get; set; }
        [Display(Name = "Empresa")]
        public List<Empresa> Empresas { get; set; }

        public int IdContrato { get; set; }
        [Display(Name = "Contrato")]
        public List<Contrato> Contratos { get; set; }


        [Display(Name = "Permissão p/ Dirigir")]
        public bool FlgMotorista { get; set; }

        public bool FlgTemporario { get; set; }

        public int IdTipoSolicitacao { get; set; }
        [Display(Name = "Tipo de Solicitação")]
        public List<TipoSolicitacao> TiposSolicitacao { get; set; }

        public int IdTipoEmissao { get; set; }
        [Display(Name = "Tipo de Emissão")]
        public TipoEmissao TiposEmissao { get; set; }


        [Display(Name = "Área")]
        public List<Area> Areas { get; set; }
        public int IdArea1 { get; set; }
        public int IdArea2 { get; set; }

        public int IdPessoa { get; set; }

        public SolicitacaoViewModel() { }

        public Solicitacao MapearParaObjetoDominio()
        {
            Solicitacao solicitacao = new Solicitacao();

            solicitacao.IdSolicitacao = solicitacao.IdSolicitacao;
            solicitacao.Empresa = new Empresa() { IdEmpresa = IdEmpresa };
            solicitacao.Contrato = new Contrato() { IdContrato = IdContrato };
            solicitacao.TipoSolicitacao = new TipoSolicitacao() { IdTipoSolicitacao = IdTipoSolicitacao };
            solicitacao.Area1 = new Area() { IdArea = IdArea1 };
            solicitacao.Area2 = new Area() { IdArea = IdArea2 };
            solicitacao.Pessoa = new Pessoa() { IdPessoa = IdPessoa };

            solicitacao.FlgTemporario = (IdTipoEmissao == 0);

            return solicitacao;
        }
    }
}