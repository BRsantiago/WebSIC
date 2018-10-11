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
        IList<Apolice> Listar();
        Apolice Obter(int id);
        Apolice ObterPorNumero(string numero, bool withTracking);
        IList<Apolice> ObterPorEmpresa(int idEmpresa);
        IList<Apolice> ObterValidas(int idEmpresa);
        Apolice Incluir(Apolice apolice);
        Apolice Atualizar(Apolice apolice);
        int Excluir(int id);
    }
}
