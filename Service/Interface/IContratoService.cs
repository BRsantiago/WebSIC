using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IContratoService
    {
        IList<Contrato> Listar();
        Contrato Obter(int id);
        Contrato ObterPorNumero(string numero);
        IList<Contrato> ObterPorEmpresa(int idEmpresa);
        IList<Contrato> ObterPorPeriodo(int idEmpresa, DateTime inicioVigencia, DateTime finalVigencia);
        IList<Contrato> ObterVigentes(int idEmpresa);
        Contrato Incluir(Contrato contrato);
        Contrato Atualizar(Contrato contrato);
        int Excluir(int id);
    }
}
