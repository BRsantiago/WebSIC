using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entity
{
    class CursoSemTurma : Base
    {
        public CursoSemTurma() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdCursoSemTurma { get; set; }
        
        [Display(Name = "Data de Validade")]
        public DateTime DataValidade { get; set; }

        public List<Curso> Cursos { get; set; }
        public Pessoa Pessoa { get; set; }

}
}
