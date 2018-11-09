using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public enum RamoAtividade
    {
        [Display(Name = "Ramo de Atividade - 1")]
        RamoAtividade1,
        [Display(Name = "Ramo de Atividade - 2")]
        RamoAtividade2,
        [Display(Name = "Ramo de Atividade - 3")]
        RamoAtividade3,
        [Display(Name = "Ramo de Atividade - 4")]
        RamoAtividade4,
    }
}
