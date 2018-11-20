using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Aeroporto : Base
    {
        public Aeroporto() : base() { }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdAeroporto { get; set; }
        [Required]
        public string Descricao { get; set; }
        public string IATA { get; set; }
        public string Sigla { get; set; }

        public IList<Empresa> Empresas { get; set; }
        public IList<Area> Areas { get; set; }
    }
}
