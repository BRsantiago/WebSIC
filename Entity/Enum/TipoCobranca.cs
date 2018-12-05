using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enum
{
    public enum TipoCobranca
    {
        [Display(Name = "Isento")]
        Isento = 1,
        [Display(Name = "Mensal")]
        Mensal = 2,
    }

}
