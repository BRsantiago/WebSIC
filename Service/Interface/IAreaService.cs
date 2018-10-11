using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAreaService
    {
        IList<Area> Listar();
        Area Obter(int id);
        Area ObterPorSigla(string sigla, bool withTracking);
        Area Incluir(Area area);
        Area Atualizar(Area area);
        int Excluir(int id);
    }
}
