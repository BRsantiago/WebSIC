using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public enum Genero
    {
        [Display(Name = "Masculino")]
        Masculino = 1,

        [Display(Name = "Feminino")]
        Feminino = 2
    }
}
