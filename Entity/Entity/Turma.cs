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

        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Curso Vinculado")]
        public Curso Curso { get; set; }

        [Required]
        [Display(Name = "Data de Realização")]
        public DateTime Realizacao { get; set; }

        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; }

        [Display(Name = "Observações Gerais")]
        public string Observacao { get; set; }

        public IList<Pessoa> Pessoas { get; set; }
    }
}
