using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSIC.Models
{
    public class RelatorioViewModel
    {

        public string DataInicial { get; set; }
        public string DataFinal { get; set; }

        public string IdAeroporto { get; set; }
        public List<Aeroporto> Aeroportos { get; set; }

        public string IdEmpresa { get; set; }
        public List<Empresa> Empresas { get; set; }
    }
}