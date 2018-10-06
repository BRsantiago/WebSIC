﻿using Entity.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Entities
{
    public class TipoCracha : Base
    {
        public TipoCracha() : base()
        {
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdTipoCracha { get; set; }

        public string Descricao { get; set; }

        public List<TipoEmpresa> TipoEmpresas { get; set; }
    }
}
