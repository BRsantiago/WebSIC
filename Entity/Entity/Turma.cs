using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Turma : Base
    {
        public Turma() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdTurma { get; set; }
        [Required]
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public string Observacao { get; set; }

        public IList<Pessoa> Pessoas { get; set; }
    }
}
