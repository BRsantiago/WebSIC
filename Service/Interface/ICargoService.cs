using Entity.DTO;
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

        ServiceReturn Incluir(Cargo cargo);
        ServiceReturn Atualizar(Cargo cargo);
        ServiceReturn Excluir(int id);
    }
}
