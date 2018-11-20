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
        public Curso() : base()
        {
            CursosSemTurma = new List<CursoSemTurma>();
        }

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
        public List<Area> Areas { get; set; }

        [Display(Name = "É obrigatório?")]
        public bool Obrigatorio { get; set; }

        [Display(Name = "Permite dirigir em áreas restritas")]
        public bool PermiteDirigirEmAreasRestritas { get; set; }

        public List<Turma> Turmas { get; set; }
        public List<CursoSemTurma> CursosSemTurma { get; set; }
        public List<RamoAtividade> RamosDeAtividade { get; set; }
    }
}
