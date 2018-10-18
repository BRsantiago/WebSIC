using Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IApoliceRepository : IRepositoryBase<Apolice>
    {
        Apolice ObterPorNumero(string numero, bool withTracking);
        IList<Apolice> ObterValidas(int idEmpresa, bool withTracking);
        IList<Apolice> ObterPorEmpresa(int idEmpresa, bool withTracking);
    }
}
