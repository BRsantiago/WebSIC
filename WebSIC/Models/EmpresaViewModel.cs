using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class EmpresaViewModel
    {
        public int IdEmpresa { get; set; }

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
        public string TipoCobranca { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public string CEP { get; set; }

        public string NumeroContrato { get; set; }

        public string Email { get; set; }

        public int IdTipoEmpresa { get; set; }

        public int IdAeroporto { get; set; }

        [Display(Name = "Área de Atuação")]
        public List<TipoEmpresa> TiposEmpresa { get; set; }

        [Display(Name = "Aeroporto")]
        public IList<Aeroporto> Aeroportos { get; set; }

        [Display(Name = "Representante")]
        public List<Pessoa> Representantes { get; set; }
    }
}