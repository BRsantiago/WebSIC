using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IContratoRepository : IRepositoryBase<Contrato>
    {
        IList<Contrato> ObterPorEmpresa(int idEmpresa);
        IList<Contrato> ObterPorPeriodo(int idEmpresa, DateTime inicioVigencia, DateTime finalVigencia);
        IList<Contrato> ObterVigentes(int idEmpresa);
        Contrato ObterPorNumero(string numero, bool withTracking);
        void IncluirNovoContrato(Contrato contrato);
    }
}
