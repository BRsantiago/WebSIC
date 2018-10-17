using Entity.DTO;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IVeiculoService 
    {
        Veiculo Obter(int id);
        Veiculo ObterPorPlaca(string placa, bool withTracking);
        Veiculo ObterPorChassi(string chassi, bool withTracking);

        ServiceReturn Incluir(Veiculo veiculo);
        ServiceReturn Atualizar(Veiculo veiculo);
        ServiceReturn Excluir(int id);

        IList<Veiculo> ObterPorApolice(int idApolice);
        IList<Veiculo> ObterPorEmpresa(int idEmpresa);
        IList<Veiculo> ObterPorAno(string ano);
        IList<Veiculo> ObterPorMarca(string marca);
        IList<Veiculo> ObterPorModelo(string modelo);
        IList<Veiculo> Listar();
    }
}
