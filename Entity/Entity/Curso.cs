using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Curso : Base
    {
        public Curso() : base() { }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdCurso { get; set; }
        public int Duracao { get; set; }
        public string Observacao { get; set; }
        [Required]
        public DateTime DataVencimento { get; set; }

        public IList<Turma> Turmas { get; set; }
        public Area Area { get; set; }
    }
}
