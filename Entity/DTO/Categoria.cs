using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public enum Categoria
    {
        [Display(Name = "A - Motocicletas")]
        A,
        [Display(Name = "B - Veículos de Passeio")]
        B,
        [Display(Name = "C - Caminhões")]
        C,
        [Display(Name = "D - Cargas Tóxicas")]
        D,
        [Display(Name = "E - Cargas Vivas")]
        E,
        [Display(Name = "AB - Motocicletas e Veículos de Passeio")]
        AB,
        [Display(Name = "AC - Motocicletas e Caminhões")]
        AC,
        [Display(Name = "AD - Motocicletas e Cargas Tóxicas")]
        AD,
        [Display(Name = "AE - Motocicletas e Cargas Vivas")]
        AE
    }
}
