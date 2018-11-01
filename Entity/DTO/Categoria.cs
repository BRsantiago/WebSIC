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
        A = 1,
        [Display(Name = "B - Veículos de Passeio")]
        B = 2,
        [Display(Name = "C - Caminhões")]
        C = 3,
        [Display(Name = "D - Cargas Tóxicas")]
        D = 4,
        [Display(Name = "E - Cargas Vivas")]
        E = 5
    }
}
