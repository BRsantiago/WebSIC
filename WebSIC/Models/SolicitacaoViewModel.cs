using Entity.DTO;
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
        public int IdAeroporto { get; set; }
        [Display(Name = "Aeroporto")]
        public List<Aeroporto> Aeroportos { get; set; }

        public int IdEmpresa { get; set; }
        [Display(Name = "Empresa")]
        public List<Empresa> Empresas { get; set; }

        public int IdContrato { get; set; }
        [Display(Name = "Contrato")]
        public List<Contrato> Contratos { get; set; }

        public bool FlgCredencial { get; set; }
        public bool FlgATIV { get; set; }

        [Display(Name = "Permissão p/ Dirigir")]
        public bool FlgMotorista { get; set; }
        public bool FlgTemporario { get; set; }

        public int IdTipoSolicitacao { get; set; }
        [Display(Name = "Tipo de Solicitação")]
        public List<TipoSolicitacao> TiposSolicitacao { get; set; }

        public int IdTipoEmissao { get; set; }
        [Display(Name = "Tipo de Emissão")]
        public List<TipoEmissao> TiposEmissao { get; set; }

        public int IdTipoCredencial { get; set; }
        [Display(Name = "Tipo de Credencial")]
        public List<TipoCredencial> TiposCredencial { get; set; }

        public int IdGenero { get; set; }
        [Display(Name = "Gênero")]
        public List<Genero> Generos { get; set; }

        [Display(Name = "Área")]
        public List<Area> Areas { get; set; }
        public int IdArea1 { get; set; }
        public int IdArea2 { get; set; }

        public string IdPessoa { get; set; }

        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }

        public string Apelido { get; set; }

        [Display(Name = "Data de Nascimento")]
        public string DataNascimento { get; set; }

        [Display(Name = "Nome do Pai")]
        public string NomePai { get; set; }

        [Display(Name = "Nome da Mãe")]
        public string NomeMae { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string UF { get; set; }

        public string CEP { get; set; }

        [Display(Name = "Tel. Emergência")]
        public string TelefoneEmergencia { get; set; }

        [Display(Name = "Tel. Residencial")]
        public string TelefoneResidencial { get; set; }

        [Display(Name = "Tel. Celular")]
        public string TelefoneCelular { get; set; }

        public string RNE { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }

        [Display(Name = "Orgão expeditor")]
        public string OrgaoExpeditor { get; set; }

        [Display(Name = "UF")]
        public string UFOrgaoExpeditor { get; set; }

        public string Genero { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        [Display(Name = "CVE")]
        public bool FlgCVE { get; set; }

        public string Email { get; set; }

        public string CNH { get; set; }

        [Display(Name = "Categoria")]
        public string CategoriaCNH { get; set; }

        [Display(Name = "Validade")]
        public string DataValidadeCNH { get; set; }
    }
}