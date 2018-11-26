using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enum
{
    public enum TipoServico
    {
        [Display(Name = "Abastecimento de Combustível")]
        Abastecimento = 1,
        [Display(Name = "Ambulância")]
        Ambulancia = 2,
        [Display(Name = "Captura e Translocação de Animais")]
        Captura = 3,
        [Display(Name = "Coleta de Resíduos Orgânicos")]
        Coleta = 4,
        [Display(Name = "Comissaria")]
        Comissaria = 5,
        [Display(Name = "Emergência")]
        Emergencia = 6,
        [Display(Name = "Fiscalização")]
        Fiscalizacao = 7,
        [Display(Name = "Manutenção")]
        Manutencao = 8,
        [Display(Name = "Movimentação de Aeronaves")]
        Movimentacao = 9,
        [Display(Name = "Veículo de Obras")]
        Obra = 10,
        [Display(Name = "Transporte de Passageiros")]
        TransportePax = 11,
        [Display(Name = "Transporte de Bagagens")]
        TransporteBag = 12,
        [Display(Name = "Transporte de Cargas")]
        TransporteCar = 13,
        [Display(Name = "Transporte de Pessoal de Serviço")]
        TransporteSer = 14,
    }
}
