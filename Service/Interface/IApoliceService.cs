using Entity.DTO;
using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IApoliceService
    {
        Apolice Obter(int id);
        Apolice ObterPorNumero(string numero, bool withTracking);

        IList<Apolice> Listar();
        IList<Apolice> ObterPorEmpresa(int idEmpresa);
        IList<Apolice> ObterValidas(int idEmpresa);

        ServiceReturn Incluir(Apolice apolice);
        ServiceReturn Atualizar(Apolice apolice);
        ServiceReturn Excluir(int id);
    }
}
