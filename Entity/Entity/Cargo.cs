using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Cargo : Base
    {
        public Cargo() : base() { }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdCargo { get; set; }

        [Display(Name = "Descrição do Cargo")]
        public string Descricao { get; set; }

        public IList<Credencial> Credenciais { get; set; }
    }
}
