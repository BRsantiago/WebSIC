using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Enum
{
    public enum Impressora
    {
        [Display(Name = "Smart 50")]
        Smart50 = 1,

        [Display(Name = "Smart 51")]
        Smart51 = 2
    }
}
