using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Area : Base
    {
        public Area() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdArea { get; set; }

        [Required]
        public string Sigla { get; set; }

        [Display(Name = "Descrição da Área")]
        public string Descricao { get; set; }

        public List<Curso> Cursos { get; set; }
    }
}
