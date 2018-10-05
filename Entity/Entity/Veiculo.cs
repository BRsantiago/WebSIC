using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class Veiculo : Base
    {
        public Veiculo() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdVeiculo { get; set; }
        public string Descricao { get; set; }
        public string Placa { get; set; }
        public string Ano { get; set; }
        public string Cor { get; set; }
        public string Observacao { get; set; }
        public string Chassi { get; set; }

        public Apolice Apolice { get; set; }
        public Empresa Empresa { get; set; }
        public IList<Solicitacao> Solicitacoes { get; set; }
        public IList<Credencial> Credenciais { get; set; }
    }
}
