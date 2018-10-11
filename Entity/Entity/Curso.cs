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

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Carga Horária")]
        public int CargaHoraria { get; set; }

        [Display(Name = "Objetivos")]
        public string Objetivos { get; set; }

        [Required]
        [Display(Name = "Validade (em dias)")]
        public int Validade { get; set; }

        [Required]
        [Display(Name = "Prazo p/ realização (em dias)")]
        public int Prazo { get; set; }

        [Display(Name = "Área vinculada")]
        public Area Area { get; set; }

        [Display(Name = "É obrigatório?")]
        public bool Obrigatorio { get; set; }

        public IList<Turma> Turmas { get; set; }
    }
}
