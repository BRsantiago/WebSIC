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
        E
    }
}
