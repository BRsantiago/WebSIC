using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class ContratoViewModel
    {
        public int IdContrato { get; set; }

        [Required]
        [Display(Name = "Número do Contrato")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Favor informar vigência inicial do contrato.")]
        [Display(Name = "Início da Vigência")]
        public DateTime InicioVigencia { get; set; }

        [Required(ErrorMessage = "Favor informar vigência final do contrato.")]
        [Display(Name = "Final da Vigência")]
        public DateTime FimVigencia { get; set; }

        public int IdEmpresa { get; set; }

        public ContratoViewModel() { }

        public ContratoViewModel(Contrato contrato)
        {
            IdContrato = contrato.IdContrato;
            Numero = contrato.Numero;
            FimVigencia = contrato.FimVigencia;
            InicioVigencia = contrato.InicioVigencia;
        }

        public Contrato MapearParaObjetoDominio()
        {
            Contrato contrato = new Contrato();

            contrato.IdContrato = this.IdContrato;
            contrato.Numero = this.Numero;
            contrato.FimVigencia = this.FimVigencia;
            contrato.InicioVigencia = this.InicioVigencia;

            contrato.Empresa = new Empresa();
            contrato.Empresa.IdEmpresa = this.IdEmpresa;

            return contrato;
        }
    }
}