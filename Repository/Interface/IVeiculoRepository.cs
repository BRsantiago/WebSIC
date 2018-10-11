using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IVeiculoRepository : IRepositoryBase<Veiculo>
    {
        IList<Veiculo> ObterPorApolice(int idApolice);
        IList<Veiculo> ObterPorEmpresa(int idEmpresa);
        IList<Veiculo> ObterPorAno(string ano);
        IList<Veiculo> ObterPorMarca(string marca);
        IList<Veiculo> ObterPorModelo(string modelo);
        Veiculo ObterPorPlaca(string placa, bool withTracking);
        Veiculo ObterPorChassi(string chassi, bool withTracking);
    }
}
