using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Schedule
    {
        public Schedule() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdSchedule { get; set; }
        public DateTime DataEnvioEmailAviso { get; set; }
        public string Descricao { get; set; }

        [Required]
        public Solicitacao Solicitacao { get; set; }
    }
}
