using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Ocorrencia : Base
    {
        public Ocorrencia() : base() { }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdOcorrencia { get; set; }
        [Required]
        public string Historico { get; set; }

        public Pessoa Pessoa { get; set; }
    }
}
