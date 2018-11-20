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
        Abastecimento,
        [Display(Name = "Ambulância")]
        Ambulancia,
        [Display(Name = "Captura e Translocação de Animais")]
        Captura,
        [Display(Name = "Coleta de Resíduos Orgânicos")]
        Coleta,
        [Display(Name = "Comissaria")]
        Comissaria,
        [Display(Name = "Emergência")]
        Emergencia,
        [Display(Name = "Fiscalização")]
        Fiscalizacao,
        [Display(Name = "Manutenção")]
        Manutencao,
        [Display(Name = "Movimentação de Aeronaves")]
        Movimentacao,
        [Display(Name = "Veículo de Obras")]
        Obra,
        [Display(Name = "Transporte de Passageiros")]
        TransportePax,
        [Display(Name = "Transporte de Bagagens")]
        TransporteBag,
        [Display(Name = "Transporte de Cargas")]
        TransporteCar,
        [Display(Name = "Transporte de Pessoal de Serviço")]
        TransporteSer,
    }
}
