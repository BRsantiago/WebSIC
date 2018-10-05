﻿using System;
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
        public string TipoCobranca { get; set; }

        [Display(Name = "Observação")]
        public string Observacao { get; set; }

        public string CEP { get; set; }

        public string NumeroContrato { get; set; }

        public string Email { get; set; }

        public IList<Pessoa> Pessoas { get; set; }
        public IList<Veiculo> Veiculos { get; set; }
        public IList<Solicitacao> Solicitacoes { get; set; }
        public IList<Credencial> Credenciais { get; set; }
        public IList<Contrato> Contratos { get; set; }
        public IList<Apolice> Apolices { get; set; }
        public IList<Aeroporto> Aeroportos { get; set; }


    }
}
