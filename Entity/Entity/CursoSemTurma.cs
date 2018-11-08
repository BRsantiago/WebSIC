using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class CursoSemTurma : Base
    {
        public CursoSemTurma() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdCursoSemTurma { get; set; }

        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; }

        [Required]
        public Curso Curso { get; set; }
        [Column("Curso_IdCurso")]
        [ForeignKey("Curso")]
        public Nullable<int> CursoId { get; set; }

        [Required]
        public Pessoa Pessoa { get; set; }
        [Column("Pessoa_IdPessoa")]
        [ForeignKey("Pessoa")]
        public Nullable<int> PessoaId { get; set; }

    }
}
