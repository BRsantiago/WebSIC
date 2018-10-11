using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICargoService
    {
        IList<Cargo> Listar();
        Cargo Obter(int id);
        Cargo Incluir(Cargo cargo);
        Cargo Atualizar(Cargo cargo);
        int Excluir(int id);
    }
}
