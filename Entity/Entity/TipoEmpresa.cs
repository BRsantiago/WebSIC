using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class TipoEmpresa : Base
    {
        public TipoEmpresa() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdTipoEmpresa { get; set; }

        public string Descricao { get; set; }

        public TipoCracha TipoCracha { get; set; }
        public List<Empresa> Empresas { get; set; }
    }
}
