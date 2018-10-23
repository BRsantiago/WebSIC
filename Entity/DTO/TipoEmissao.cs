using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    //public class TipoEmissao
    //{
    //    public int IdTipoEmissao { get; set; }
    //    public string Descricao { get; set; }
    //}

    public enum TipoEmissao
    {
        [Display(Name = "Temporária")]
        Temporaria,
        [Display(Name = "Definitiva")]
        Definitiva,
    }
}
