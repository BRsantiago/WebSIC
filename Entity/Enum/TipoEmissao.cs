using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enum
{
    public enum TipoEmissao
    {
        [Display(Name = "Temporária")]
        Temporaria = 1,
        [Display(Name = "Definitiva")]
        Definitiva = 2,
    }
}
