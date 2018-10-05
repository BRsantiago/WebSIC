using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public abstract class Base
    {
        public Base()
        {
            Criacao = DateTime.Now;
            Atualizacao = DateTime.Now;
            Ativo = true;
        }


        [Display(Name = "Data de Criação")]
        public DateTime Criacao { get; set; }

        [Display(Name = "Criado Por")]
        public string Criador { get; set; }

        [Display(Name = "Data de Atualização")]
        public DateTime Atualizacao { get; set; }

        [Display(Name = "Atualizado Por")]
        public string Atualizador { get; set; }

        [Display(Name = "Ativo/Inativo")]
        public bool Ativo { get; set; }

    }
}
