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
        Contrato ObterPorNumero(string numero, bool withTracking);
        IList<Contrato> ObterPorEmpresa(int idEmpresa);
        IList<Contrato> ObterPorPeriodo(int idEmpresa, DateTime inicioVigencia, DateTime finalVigencia);
        IList<Contrato> ObterVigentes(int idEmpresa);
        void Incluir(Contrato contrato);
        Contrato Atualizar(Contrato contrato);
        int Excluir(int id);
        List<Contrato> ObterTodos();
    }
}
