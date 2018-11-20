using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class TipoSolicitacao : Base
    {
        public TipoSolicitacao() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdTipoSolicitacao { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public bool FlgDesativaCredencial { get; set; }
        public bool FlgGeraNovaCredencial { get; set; }
    }
}
